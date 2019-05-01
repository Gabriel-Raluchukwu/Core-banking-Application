using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using BankTwo.Application.Logic.InterfaceClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class TransactionLogic:ITransactionLogic
    {
        ITransactionRepository transactionRepository;
        IGLAccountLogic gLAccountLogic;
        IGLCategoryLogic gLCategoryLogic;
        IEODLogic EODLogic;
        public TransactionLogic(IEODLogic eODLogic,
            ITransactionRepository transaction,IGLAccountLogic gLogic
            ,IGLCategoryLogic gLCategory)
        {
            EODLogic = eODLogic;
            transactionRepository = transaction;
            gLAccountLogic = gLogic;
            gLCategoryLogic = gLCategory;
        }
        public async Task<bool> AddPosting(TransactionViewModel transactionViewModel)
        {
            var transaction = Mapper.Map<TransactionViewModel, Transaction>(transactionViewModel);
            return await transactionRepository.AddPostingToDB(transaction);
        }

        public IEnumerable<TransactionViewModel> ViewPostings()
        {
            var transactions = transactionRepository.RetrieveAllPostingsFromDB();
            if (transactions != null)
            {
              return  transactions.Select(Mapper.Map<Transaction, TransactionViewModel>);
            }
            return null;
        }
        public IEnumerable<GLAccountDropDownViewModel> PopulateDropDown()
        {
            return gLAccountLogic.populateGLAccountDropDown();
        }
        public bool PostToGLAccount(TransactionViewModel transactionViewModel)
        {
            transactionViewModel.TransactionDate = EODLogic.RetrieveFinancialDate();
            var transaction = Mapper.Map<TransactionViewModel, Transaction>(transactionViewModel);
            int debitAccountId = transaction.DebitGLAccountId;
            int creditAccountId = transaction.CreditGLAccountId;

            GLAccount gLAccountDebit = gLAccountLogic.RetrieveGLAccount(debitAccountId);
            GLAccount glAccountCredit = gLAccountLogic.RetrieveGLAccount(creditAccountId);

            bool debitCheck = DebitAccount(gLAccountDebit,transaction.DebitAmount,MainCategoryOperation(gLAccountDebit));
            bool creditCheck = CreditAccount(glAccountCredit, transaction.CreditAmount, MainCategoryOperation(glAccountCredit));
            if (debitCheck && creditCheck)
            {
                gLAccountLogic.UpdateAccountBalance(gLAccountDebit);
                gLAccountLogic.UpdateAccountBalance(glAccountCredit);
                return true;
            }
            return false;
        }
        private int MainCategoryOperation(GLAccount gLAccount)
        {
            int categoryId = gLAccount.GLCategoryId;
            var category = gLCategoryLogic.RetrieveGLCategory(categoryId);
            return category.MainCategory.MainCategoryOperation;
        }
        private bool DebitAccount(GLAccount gLAccount,decimal debitAmount, int operationType)
        {
            if (operationType < 0)
            {
                if (gLAccount.AccountBalance > debitAmount)
                {
                    debitAmount = debitAmount * operationType;
                    gLAccount.AccountBalance = gLAccount.AccountBalance + debitAmount;
                    return true;
                }
                return false;
            }

            debitAmount = debitAmount * operationType;
            gLAccount.AccountBalance = gLAccount.AccountBalance + debitAmount;
            return true;
        }
        private bool CreditAccount(GLAccount gLAccount,decimal creditAmount,int operationType)
        {
            operationType = operationType * -1;
            if (operationType < 0)
            {
                if (gLAccount.AccountBalance > creditAmount)
                {
                    creditAmount = creditAmount * -1;
                    gLAccount.AccountBalance = gLAccount.AccountBalance + creditAmount;
                    return true;
                }
                return false;
            }
            creditAmount = creditAmount * operationType;
            gLAccount.AccountBalance = gLAccount.AccountBalance + creditAmount;
            return true;
        }

    }
    
}
