using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class LoanAccountRepository:ILoanAccountRepository
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public LoanAccountRepository()
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
        public async Task<bool> AddLoanAccountToDB(LoanAccount loanAccount)
        {
            dbContext.LoanAccounts.Add(loanAccount);
            return await databasePersistence.PersistToDatabase();
        }

        public LoanAccount RetrieveById(int id)
        {
            LoanAccount loanAccount = null;
            try
            {
                loanAccount = dbContext.LoanAccounts.SingleOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return loanAccount;
        }

        public async Task<bool> UpdateDB(LoanAccount loanAccount)
        {
            dbContext.Entry(loanAccount).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }

        public IEnumerable<LoanAccount> RetrieveAllAccounts()
        {
            IEnumerable<LoanAccount> loanAccounts;
            try
            {
                loanAccounts = dbContext.LoanAccounts.Include(m => m.Branch).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return loanAccounts;
        }
        public async Task<bool> UpdateLoanAccount(LoanAccount loanAccount)
        {
            dbContext.Entry(loanAccount).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }
    }
}
