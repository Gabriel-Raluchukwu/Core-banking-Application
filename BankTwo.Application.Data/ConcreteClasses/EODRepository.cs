using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class EODRepository:IEODRepository
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public EODRepository()
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

        public async Task<bool> AddEODEntryToDB(EOD EODEntry)
        {
            dbContext.EOD.Add(EODEntry);
            return await databasePersistence.PersistToDatabase();
        }

        public EOD RetrieveLastEntry()
        {
            EOD lastEntry;
            try
            {
                lastEntry = dbContext.EOD.OrderByDescending(p => p.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return lastEntry;
        }
        public async Task<bool> UpdateEODEntry(EOD eodUpdate)
        {
            dbContext.Entry(eodUpdate).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();         
        }
        //public IEnumerable<EOD> RetrieveAllEODEntries()
        //{
        //    IEnumerable<EOD> EODEntries;
        //    try
        //    {
        //        EODEntries = dbContext.EOD.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
        //        return null;
        //    }
        //    return EODEntries;
        //}

    }
}
