using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class BankBranch : IBranch<Branch>
    {
        public const bool Failure = false;
        public const bool Success = true;

        private static ApplicationDbContext dbContext;
        private DatabasePersistence databasePersistence;
        public BankBranch()
        {
            if (dbContext == null)
            {
                dbContext = new ApplicationDbContext();
            }
            databasePersistence = new DatabasePersistence(dbContext);
        }
        public Task<bool> DeleteFromDB(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertToDB(Branch branch)
        {
            dbContext.Branch.Add(branch);
            return await databasePersistence.PersistToDatabase();
          
        }

        public IEnumerable<Branch> RetrieveAllFromDB()
        {
            IEnumerable<Branch> AllBranches;
            try
            {
              AllBranches = dbContext.Branch.ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return AllBranches;
        }

        public Branch RetrieveById(int item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDB(Branch item)
        {
            throw new NotImplementedException();
        }
    }
}
