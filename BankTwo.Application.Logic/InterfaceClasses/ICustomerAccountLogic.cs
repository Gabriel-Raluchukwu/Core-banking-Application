using BankTwo.Application.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface ICustomerAccountLogic
    {
        Task<bool> AddCustomerAccount(CustomerAccountViewModel customerAccountViewModel);
        
        IEnumerable<CustomerAccountViewModel> CustomerAccountDisplay();
 
        CustomerAccountViewModel RetrieveCustomerAccountById(string id);

        CustomerAccount RetrieveCustomerAccount(int id);

        Task<bool> EditCustomerAccount(CustomerAccountViewModel customerAccountViewModel);

        Task<bool> UpdateAccountBalance(CustomerAccount customerAccount);

        Task<bool> DeleteCustomerAccount(string id);

        IEnumerable<CustomerAccountViewModel> RetrieveOpenCustomerAccount();

        Task<bool> CloseAccount(string id);

        //Helper Methods
        IEnumerable<BranchViewModel> PopulateBranchDropDown();

    }
}
