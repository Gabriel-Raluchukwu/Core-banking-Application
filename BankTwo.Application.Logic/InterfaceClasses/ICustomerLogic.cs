using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface ICustomerLogic
    {

        Task<bool> AddCustomer(CustomerViewModel customerViewModel);

        IEnumerable<CustomerViewModel> CustomerDisplay();

        CustomerViewModel RetrieveCustomerById(string id);
           
        Task<bool> EditCustomer(CustomerViewModel customerViewModel);

        Task<bool> DeleteCustomer(string id);

        bool CheckForDuplicateEmail(string Email);

        bool CheckForDuplicates(int identifcationNumber);


    }
}
