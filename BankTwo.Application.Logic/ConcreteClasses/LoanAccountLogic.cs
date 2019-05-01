using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using BankTwo.Application.Logic.InterfaceClasses;
using BankTwo.Application.Logic.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class LoanAccountLogic:ILoanAccountLogic
    {
        ILoanAccountRepository loanAccountRepository;
        IAccountConfigurationRepository accountConfigurationRepository;
        ICustomerAccountRepository<CustomerAccount> customerAccountRepository;
        public LoanAccountLogic(ILoanAccountRepository loanRepo,IAccountConfigurationRepository configurationRepository,
            ICustomerAccountRepository<CustomerAccount> customerAccountRepo,ITellerPost teller)
        {
            loanAccountRepository = loanRepo;
            accountConfigurationRepository = configurationRepository;
            customerAccountRepository = customerAccountRepo;
        }
        public async Task<bool> SaveLoanAccount(LoanAccountViewModel loanAccountViewModel)
        { 
            var loanAccountConfig = accountConfigurationRepository.RetrieveLoanConfiguration();
            var interestRate = loanAccountConfig.DebitInterestRate / 100;
            var yearly= (loanAccountViewModel.LoanPrincipal * (decimal)(interestRate) * (decimal)ConvertToYears(loanAccountViewModel.LoanDuration));
            loanAccountViewModel.LoanAccountBalance = loanAccountViewModel.LoanPrincipal + yearly;
            loanAccountViewModel.MonthlyLoanBalance = loanAccountViewModel.LoanAccountBalance / 12;
           
            var loanAccount = Mapper.Map<LoanAccountViewModel, LoanAccount>(loanAccountViewModel);
            bool check = LoanTransaction(loanAccountViewModel);

            return await loanAccountRepository.AddLoanAccountToDB(loanAccount);

        }
        public async Task<bool> EditLoanAccount(LoanAccountViewModel loanAccountViewModel)
        {
            int id = int.Parse(Encrypt.Decode(loanAccountViewModel.EncryptedId));
            var LonAccountToUpdate = loanAccountRepository.RetrieveById(id);

            //Manual Mapping
            LonAccountToUpdate.CAccountName = loanAccountViewModel.CAccountName;
            LonAccountToUpdate.BranchId = loanAccountViewModel.BranchId;

            return await loanAccountRepository.UpdateDB(LonAccountToUpdate);
        }

        public IEnumerable<LoanAccountDisplayViewModel> RetrieveAllAccounts()
        {
            var loanAccounts = loanAccountRepository.RetrieveAllAccounts().Where(m => m.IsClosed == false).
                Select(Mapper.Map<LoanAccount,LoanAccountDisplayViewModel>).ToList();
            return loanAccounts;
        }

        public LoanAccountViewModel RetrieveLoanAccountById(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            var retrievedLoanAccount = loanAccountRepository.RetrieveById(Id);
            if (retrievedLoanAccount == null)
            {
                return null;
            }
            var loanAccountViewModel = Mapper.Map<LoanAccount, LoanAccountViewModel>(retrievedLoanAccount);

            return loanAccountViewModel;
        }

        public async Task<bool> CloseAccount(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            var loanAccountToUpdate = loanAccountRepository.RetrieveById(Id);

            //Manual Mapping
            loanAccountToUpdate.IsClosed = true;

            return await loanAccountRepository.UpdateDB(loanAccountToUpdate);
        }
        private bool LoanTransaction(LoanAccountViewModel loanAccountViewModel)
        {
            int AssetOperationType = 1;
            int LiabilitiesOperationType = 1;
            var customerAccount =  customerAccountRepository.RetrieveById(loanAccountViewModel.CustomerAccountId);
            loanAccountViewModel.CustomerAccountNumber = customerAccount.CAccountNumber;
            var debitCheck = DebitLoanAccount(loanAccountViewModel,loanAccountViewModel.LoanPrincipal,AssetOperationType);
            var creditCheck = CreditCustomerAccount(customerAccount,loanAccountViewModel.LoanPrincipal,LiabilitiesOperationType);
            if (debitCheck && creditCheck)
            {
                customerAccountRepository.UpdateDB(customerAccount);
                return true;
            }
            return false;
        }
        public bool DebitLoanAccount(LoanAccountViewModel loanAccountViewModel,decimal Amount, int OperationType)
        {
            Amount = Amount * OperationType;
            loanAccountViewModel.LoanAccountBalance = loanAccountViewModel.LoanAccountBalance + Amount;
            return true;
        }
        //public bool CreditLoanAccount(LoanAccountViewModel loanAccountViewModel, decimal Amount, int OperationType)
        //{
        //    Amount = Amount * OperationType;
        //    if (loanAccountViewModel.LoanAccountBalance > Amount)
        //    {
        //        loanAccountViewModel.LoanAccountBalance = loanAccountViewModel.LoanAccountBalance + Amount;
        //        return true;
        //    }
            
        //}
        public bool CreditCustomerAccount(CustomerAccount account, decimal Amount, int OperationType)
        {
            Amount = Amount * OperationType;
            account.AccountBalance = account.AccountBalance + Amount;
            return true;
        }
        private double ConvertToYears(int loanDuration)
        {
            double yearsConversation = (loanDuration * 1) /12;
            return yearsConversation;
        }
    }
}
