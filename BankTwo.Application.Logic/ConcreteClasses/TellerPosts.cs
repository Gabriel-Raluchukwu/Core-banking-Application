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
    public class TellerPosts:ITellerPost
    {
        IAccountConfigurationRepository configurationRepository;
        ICustomerAccountRepository<CustomerAccount> customerAccountRepository;
        ITellerPostRepository tellerPostRepository;
        IGLAccountRepository<GLAccount> gLAccountRepository;
        IGLCategoryLogic gLCategoryLogic;
        public TellerPosts(ITellerPostRepository tellerPostRepo, IGLAccountRepository<GLAccount> gLAccountRepo, ICustomerAccountRepository<CustomerAccount> customerAccntRepo
            , IAccountConfigurationRepository configurationRepo,IGLCategoryLogic gLCategoryLog)
        {
            tellerPostRepository = tellerPostRepo;
            gLAccountRepository = gLAccountRepo;
            customerAccountRepository = customerAccntRepo;
            configurationRepository = configurationRepo;
            gLCategoryLogic = gLCategoryLog;
        }

        public async Task<bool> SaveTellerPosting(TellerPostViewModel tellerPostViewModel)
        {
            var tellerPost = Mapper.Map<TellerPostViewModel,TellerPost>(tellerPostViewModel);
            return await tellerPostRepository.AddTellerPostToDB(tellerPost);
        }
        public IEnumerable<TellerPostViewModel> DisplayTellerPostings()
        {
            IEnumerable<Core.Entities.TellerPost> tellerPosts = tellerPostRepository.RetrieveAllTellerPosts();
            if (tellerPosts == null)
            {
                return null;
            }
            var tellerPostsViewModels = tellerPosts.Select(Mapper.Map<Core.Entities.TellerPost, TellerPostViewModel>);
            return tellerPostsViewModels;
        }
        
        public bool TellerPosting(TellerPostViewModel tellerViewModel)
        {
            int LiabilitiesOperationType = -1;

            if (tellerViewModel.TellerPostingType == TellerPostingType.Deposit)
            {
                int DebitAccount = tellerViewModel.GLAccountId;
                int CreditAccount = tellerViewModel.CustomerAccountId;

                GLAccount gLAccount = gLAccountRepository.RetrieveById(DebitAccount);
                CustomerAccount customerAccount = customerAccountRepository.RetrieveById(CreditAccount);

                if (customerAccount.IsClosed == false)
                {
                    //Debit the GL Account
                    bool DebitCheck = DebitGLAccount(gLAccount, tellerViewModel.DebitAmount, MainCategoryOperation(gLAccount));

                    //Credit the  Customer Account
                    bool CreditCheck = CreditCustomerAccount(customerAccount, tellerViewModel.CreditAmount, LiabilitiesOperationType);


                    if (DebitCheck && CreditCheck)
                    {
                        gLAccountRepository.UpdateDB(gLAccount);
                        customerAccountRepository.UpdateDB(customerAccount);
                        return true;
                    }
                }
                return false;
            }
            if (tellerViewModel.TellerPostingType == TellerPostingType.Withdrawal)
            {
                int DebitAccount = tellerViewModel.CustomerAccountId;
                int CreditAccount = tellerViewModel.GLAccountId;

                GLAccount gLAccount = gLAccountRepository.RetrieveById(CreditAccount);
                CustomerAccount customerAccount = customerAccountRepository.RetrieveById(DebitAccount);

                if (customerAccount.IsClosed == false)
                { 

                    bool CreditCheck = CreditGLAccount(gLAccount, tellerViewModel.CreditAmount, MainCategoryOperation(gLAccount));
                    bool DebitCheck = DebitCustomerAccount(customerAccount, tellerViewModel.DebitAmount, LiabilitiesOperationType);
                    if (customerAccount.AccountTypeEnum == Core.Entities.AccountTypeEnum.Current)
                    {
                        customerAccount.Charges += CalculateCOTCharge(tellerViewModel.DebitAmount);
                    }
                    if (CreditCheck && DebitCheck)
                    {
                        customerAccount.NumberOfWithdrawals = customerAccount.NumberOfWithdrawals + 1;
                        gLAccountRepository.UpdateDB(gLAccount);
                        customerAccountRepository.UpdateDB(customerAccount);
                        return true;
                    }
                   
                }
            }
            return false;
        }
        public decimal CalculateCOTCharge(decimal AmountWithdrawn)
        {
            var COTCharge = AmountWithdrawn * (decimal)configurationRepository.RetrieveCurrentConfiguration().COT/1000;
            return COTCharge;
        }
        public bool DebitGLAccount(GLAccount account, decimal Amount, int OperationType)
        {
            Amount = Amount * OperationType;
            account.AccountBalance = account.AccountBalance + Amount;
            return true;
        }
        public bool CreditGLAccount(GLAccount account, decimal Amount, int OperationType)
        {
            OperationType = OperationType * -1;
            if (account.AccountBalance > Amount)
            {
                Amount = Amount * OperationType;
                account.AccountBalance = account.AccountBalance + Amount;
                return true;
            }
            return false;
        }
        public bool DebitCustomerAccount(CustomerAccount account, decimal Amount, int OperationType)
        {
            if (account.AccountTypeEnum == Core.Entities.AccountTypeEnum.Current && account.AccountBalance >= configurationRepository.RetrieveCurrentConfiguration().CurrentMinimumBalance + Amount)
            {
                if (account.AccountBalance >= Amount)
                {
                    Amount = Amount * OperationType;
                    account.AccountBalance = account.AccountBalance + Amount;
                    return true;
                }
            }
            if (account.AccountTypeEnum == Core.Entities.AccountTypeEnum.Savings && account.AccountBalance >= configurationRepository.RetrieveSavingsConfiguration().SavingsMinimumBalance + Amount)
            {
                if (account.AccountBalance >= Amount)
                {
                    Amount = Amount * OperationType;
                    account.AccountBalance = account.AccountBalance + Amount;
                    return true;
                }
            }
            return false;
        }
        public bool CreditCustomerAccount(CustomerAccount account, decimal Amount, int OperationType)
        {
            OperationType = OperationType * -1;
            Amount = Amount * OperationType;
            account.AccountBalance = account.AccountBalance + Amount;
            return true;
        }
        private int MainCategoryOperation(GLAccount gLAccount)
        {
            int categoryId = gLAccount.GLCategoryId;
            var category = gLCategoryLogic.RetrieveGLCategory(categoryId);
            return category.MainCategory.MainCategoryOperation;
        }
    }
}
