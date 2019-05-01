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
    public class CustomerAccountRepository: ICustomerAccountRepository<CustomerAccount>
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public CustomerAccountRepository()
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
            var customerAccountToDelete = dbContext.CustomerAccount.Find(id);
            dbContext.CustomerAccount.Remove(customerAccountToDelete);
            return await databasePersistence.PersistToDatabase();
        }

        public async Task<bool> InsertToDB(CustomerAccount customerAccount)
        {
            dbContext.CustomerAccount.Add(customerAccount);
            return await databasePersistence.PersistToDatabase();
        }

        public IEnumerable<CustomerAccount> RetrieveAllFromDB()
        {
            IEnumerable<CustomerAccount> customerAccounts;
            try
            {
                customerAccounts = dbContext.CustomerAccount.Include(b => b.Branch).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return customerAccounts;
        }

        public CustomerAccount RetrieveById(int id)
        {
            CustomerAccount customerAccount = null;
            try
            {
                customerAccount = dbContext.CustomerAccount.SingleOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return customerAccount;
        }

        public async Task<bool> UpdateDB(CustomerAccount customerAccount)
        {
            dbContext.Entry(customerAccount).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }

        public IEnumerable<CustomerAccount> RetrieveOpenAccounts()
        {
            var openAccounts = dbContext.CustomerAccount.Where(x => x.IsClosed == false).Include(b => b.Branch).ToList();
            return openAccounts;
        }
        public CustomerAccount RetrieveByAccountNumber(int accountNumber)
        {
            CustomerAccount customerAccount = null;
            try
            {
                customerAccount = dbContext.CustomerAccount.SingleOrDefault(i => i.CAccountNumber == accountNumber);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null ;
            }
            return customerAccount;
        }
    }
}
