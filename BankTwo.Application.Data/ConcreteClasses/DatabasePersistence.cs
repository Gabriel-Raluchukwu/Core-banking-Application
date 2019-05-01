using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class DatabasePersistence
    {
        private bool Failure = false;
        private bool Success = true;
        private ApplicationDbContext dbContext;
        public DatabasePersistence(ApplicationDbContext db)
        {
            dbContext = db;
        }

        public async Task<bool> PersistToDatabase()
        {
            try
            {
              await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return Failure;
            }
            //finally { }

            return Success;
        }
    }
}
