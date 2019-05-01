using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using BankTwo.Application.Logic.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class CustomerAccountLogic:ICustomerAccountLogic
    {
        ICustomerAccountRepository<CustomerAccount> customerAccountRepository;
        IBranchLogic branchLogic;
        public CustomerAccountLogic(ICustomerAccountRepository<CustomerAccount> customerAccount,
            IBranchLogic branch)
        {
            this.customerAccountRepository = customerAccount;
            this.branchLogic = branch;
        }
        public async Task<bool> AddCustomerAccount(CustomerAccountViewModel customerAccountViewModel)
        {
           // customerAccountViewModel.CAccountNumber = AutoGenerator.GenerateCustomerIdenticationNo(customerAccountViewModel.CustomerId, (int)customerAccountViewModel.AccountTypeEnum);
            var customerAccount = Mapper.Map<CustomerAccountViewModel, CustomerAccount>(customerAccountViewModel);

            return await customerAccountRepository.InsertToDB(customerAccount);

        }
        public IEnumerable<CustomerAccountViewModel> CustomerAccountDisplay()
        {
            IEnumerable<CustomerAccountViewModel> customerAccountViewModels;
            var accounts = customerAccountRepository.RetrieveAllFromDB();
            if (accounts != null)
            {
                customerAccountViewModels = accounts.Select(Mapper.Map<CustomerAccount, CustomerAccountViewModel>).ToList();
                return customerAccountViewModels;
            }
            return null;
        }
        public CustomerAccountViewModel RetrieveCustomerAccountById(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            var retrievedCustomerAccount = customerAccountRepository.RetrieveById(Id);
            if (retrievedCustomerAccount == null)
            {
                return null;
            }
            var customerAccountViewModel = Mapper.Map<CustomerAccount, CustomerAccountViewModel>(retrievedCustomerAccount);

            return customerAccountViewModel;
        }
        public CustomerAccount RetrieveCustomerAccount(int id)
        {
            var retrievedCustomerAccount = customerAccountRepository.RetrieveById(id);
            if (retrievedCustomerAccount == null)
            {
                return null;
            }
            return retrievedCustomerAccount;
        }
        public async Task<bool> EditCustomerAccount(CustomerAccountViewModel customerAccountViewModel)
        {
            int id = int.Parse(Encrypt.Decode(customerAccountViewModel.EncryptedId));
            var customerAccountToUpdate = customerAccountRepository.RetrieveById(id);

            //Manual Mapping
            customerAccountToUpdate.CAccountName = customerAccountViewModel.CAccountName;
            customerAccountToUpdate.BranchId = customerAccountViewModel.BranchId;

            return await customerAccountRepository.UpdateDB(customerAccountToUpdate);
        }
        public async Task<bool> UpdateAccountBalance(CustomerAccount customerAccount)
        {
            var customerAccountToUpdate = customerAccountRepository.RetrieveById(customerAccount.Id);

            //Manual Mapping
            customerAccountToUpdate.AccountBalance = customerAccount.AccountBalance;
            return await customerAccountRepository.UpdateDB(customerAccountToUpdate);
        }

        public async Task<bool> DeleteCustomerAccount(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            return await customerAccountRepository.DeleteFromDB(Id);
        }

        public IEnumerable<CustomerAccountViewModel> RetrieveOpenCustomerAccount()
        {
            IEnumerable<CustomerAccountViewModel> customerAccountViewModels;
            var openAccounts = customerAccountRepository.RetrieveOpenAccounts();
            if (openAccounts != null)
            {
                customerAccountViewModels = openAccounts.Select(Mapper.Map<CustomerAccount, CustomerAccountViewModel>).ToList();
                return customerAccountViewModels;
            }
            return null;
        }
        //Helper Methods
        public IEnumerable<BranchViewModel> PopulateBranchDropDown()
        {
            return branchLogic.PopulateBranchDropDownList();
        }
        public async Task<bool> CloseAccount(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            var customerAccountToUpdate = customerAccountRepository.RetrieveById(Id);

            //Manual Mapping
            customerAccountToUpdate.IsClosed = true; 

            return await customerAccountRepository.UpdateDB(customerAccountToUpdate);
        }
     
    }
}
