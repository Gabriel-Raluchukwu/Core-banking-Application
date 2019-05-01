using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using BankTwo.Application.Logic.Utilities;
using BankTwo.Application.Logic.InterfaceClasses;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class CustomerLogic:ICustomerLogic
    {
        ICustomerRepository<Customer> customerRepository;
        public CustomerLogic(ICustomerRepository<Customer> customerRepo)
        {
            this.customerRepository = customerRepo;
        }
        public async Task<bool> AddCustomer(CustomerViewModel customerViewModel)
        {
            var glAccount = Mapper.Map<CustomerViewModel, Customer>(customerViewModel);

            return await customerRepository.InsertToDB(glAccount);

        }
        public IEnumerable<CustomerViewModel> CustomerDisplay()
        {
            IEnumerable<CustomerViewModel> customerViewModels;
            var accounts = customerRepository.RetrieveAllFromDB();
            if (accounts != null)
            {
                customerViewModels = accounts.Select(Mapper.Map<Customer, CustomerViewModel>).ToList();
                return customerViewModels;
            }
            return null;
        }
        public CustomerViewModel RetrieveCustomerById(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            var retrievedCustomer = customerRepository.RetrieveById(Id);
            if (retrievedCustomer == null)
            {
                return null;
            }
            var customerViewModel = Mapper.Map<Customer, CustomerViewModel>(retrievedCustomer);

            return customerViewModel;
        }
        public async Task<bool> EditCustomer(CustomerViewModel customerViewModel)
        {
            int id = int.Parse(Encrypt.Decode(customerViewModel.EncryptedId));
            var customerToUpdate = customerRepository.RetrieveById(id);
            Mapper.Map<CustomerViewModel, Customer>(customerViewModel, customerToUpdate);
            return await customerRepository.UpdateDB(customerToUpdate);
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            return await customerRepository.DeleteFromDB(Id);
        }
        public bool CheckForDuplicates(int identifcationNumber)
        {
            var retrievedCustomer = customerRepository.RetrieveByIdentificationNumber(identifcationNumber);
            if (retrievedCustomer != null)
            {
                return true;
            }
            return false;
        }
        public bool CheckForDuplicateEmail(string Email)
        {
           var emails = customerRepository.RetrieveCustomersEmail();
            if (emails.Contains(Email.ToLower()))
            {
                return true;
            }
            return false;
        }
            
    }
}
