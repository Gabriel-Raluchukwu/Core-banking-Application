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
    public class GLCategoryRepository : IGLCategoryRepository<GLCategory>
    {
        public const bool Failure = false;
        public const bool Success = true;

        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;

        public GLCategoryRepository()
        {
            if (dbContext == null)
            {
                dbContext = new ApplicationDbContext();
            }
            databasePersistence = new DatabasePersistence(dbContext);
        }
        public async Task<bool> DeleteFromDB(int id)
        {
            var AccountToDelete = dbContext.GLCategories.Find(id);
            dbContext.GLCategories.Remove(AccountToDelete);
            return await databasePersistence.PersistToDatabase();
        }

        public async Task<bool> InsertToDB(GLCategory gLCategory)
        {
            dbContext.GLCategories.Add(gLCategory);
            return await databasePersistence.PersistToDatabase();  
        }

        public IEnumerable<GLCategory> RetrieveAllFromDB()
         {
            IEnumerable<GLCategory> gLCategories;
            try
            {
                 gLCategories = dbContext.GLCategories.Include(m => m.MainCategory).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
           return gLCategories;
            
        }

        public GLCategory RetrieveById(int id)
        {
            GLCategory gLCategory = null;
            try
            {
                gLCategory = dbContext.GLCategories.Include(m => m.MainCategory).SingleOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return gLCategory;
        }

        public async Task<bool> UpdateDB(GLCategory gLCategory)
        {
            dbContext.Entry(gLCategory).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }
    }
}
