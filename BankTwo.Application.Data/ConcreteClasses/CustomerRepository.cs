using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class CustomerRepository : ICustomerRepository<Customer>
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public CustomerRepository()
        {
            if (dbContext == null)
            {
                dbContext = new ApplicationDbContext();
            }
            if (databasePersistence == null)
            {
                databasePersistence = new DatabasePersistence(dbContext);
            }
        }

        public async Task<bool> DeleteFromDB(int id)
        {
            var customerToDelete = dbContext.Customers.Find(id);
            dbContext.Customers.Remove(customerToDelete);
            return await databasePersistence.PersistToDatabase();
        }

        public async Task<bool> InsertToDB(Customer customer)
        {
            dbContext.Customers.Add(customer);
            return await databasePersistence.PersistToDatabase();
        }

        public IEnumerable<Customer> RetrieveAllFromDB()
        {
            IEnumerable<Customer> customers;
            try
            {
                customers = dbContext.Customers.ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return customers;
        }

        public Customer RetrieveById(int id)
        {
            Customer customer = null;
            try
            {
                customer = dbContext.Customers.SingleOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return customer;
        }
        public Customer RetrieveByIdentificationNumber(int id)
        {
            Customer customer = dbContext.Customers.SingleOrDefault(i => i.IdentificationNumber == id);
            return customer;
        }

        public async Task<bool> UpdateDB(Customer customer)
        {
            dbContext.Entry(customer).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }
        public List<string> RetrieveCustomersEmail()
        {
            var Emails = from Customers in dbContext.Users
                             select new { Customers.Email};
            List<string> CustomersEmail = new List<string>();
            foreach (var row in Emails)
            {
                CustomersEmail.Add(row.Email.ToLower());
            }
            return CustomersEmail;

        }
    }
}
