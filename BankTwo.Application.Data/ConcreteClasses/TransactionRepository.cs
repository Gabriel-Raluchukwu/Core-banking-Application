using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class TransactionRepository:ITransactionRepository
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public TransactionRepository()
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
       public async Task<bool> AddPostingToDB(Transaction transaction)
        {
            dbContext.Transactions.Add(transaction);
            return await databasePersistence.PersistToDatabase();
        }
        public IEnumerable<Transaction> RetrieveAllPostingsFromDB()
        {
            IEnumerable<Transaction> transactions;
            try
            {
                transactions = dbContext.Transactions.Include(m => m.DebitGLAccount).Include(m => m.CreditGLAccount).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return transactions;
        }

    }
}
