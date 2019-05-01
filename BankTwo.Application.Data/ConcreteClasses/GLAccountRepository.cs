using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class GLAccountRepository : IGLAccountRepository<GLAccount>
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;

        public GLAccountRepository()
        {
            if(dbContext == null)
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
            var gLAccountToDelete = dbContext.GLAccounts.Find(id);
            dbContext.GLAccounts.Remove(gLAccountToDelete);
            return await databasePersistence.PersistToDatabase();
        }

        public async Task<bool> InsertToDB(GLAccount glAccount)
        {
            dbContext.GLAccounts.Add(glAccount);
            return await databasePersistence.PersistToDatabase();
        }

        public IEnumerable<GLAccount> RetrieveAllFromDB()
        {
            IEnumerable<GLAccount> gLAccounts;
            try
            {
                gLAccounts = dbContext.GLAccounts.Include(c => c.GLCategory).Include(b => b.Branch).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return gLAccounts;
        }

        public GLAccount RetrieveById(int id)
        {
            GLAccount gLAccount;
            try
            {
                gLAccount = dbContext.GLAccounts.Include(m => m.GLCategory).SingleOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;               
            }
            return gLAccount;
        }

        public async Task<bool> UpdateDB(GLAccount gLAccount)
        {
            dbContext.Entry(gLAccount).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }
    }
}
