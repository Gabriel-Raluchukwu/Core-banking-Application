using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class TellerPostRepository:ITellerPostRepository
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public TellerPostRepository()
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
        public async Task<bool> AddTellerPostToDB(TellerPost tellerTransaction)
        {
            dbContext.TellerPosts.Add(tellerTransaction);
            return await databasePersistence.PersistToDatabase();
        }
        public IEnumerable<TellerPost> RetrieveAllTellerPosts()
        {
            IEnumerable<TellerPost> tellerTransactions;
            try
            {
                tellerTransactions = dbContext.TellerPosts.Include(m => m.CustomerAccount).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return tellerTransactions;
        }
    }
}
