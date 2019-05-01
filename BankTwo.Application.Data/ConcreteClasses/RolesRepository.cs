using BankTwo.Application.Data.InterfaceClasses;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class RolesRepository : IRoleRepository<IdentityRole>
    {
        public const bool Failure = false;
        public const bool Success = true;

        private static ApplicationDbContext dbContext;
        public RolesRepository()
        {
            dbContext = new ApplicationDbContext();
        }
        public Task<bool> DeleteFromDB(int item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertToDB(IdentityRole item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdentityRole> RetrieveAllFromDB()
        {
            IEnumerable<IdentityRole> AllRoles;
            try
            {
                AllRoles = dbContext.Roles.ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return AllRoles;
        }

        public IdentityRole RetrieveById(int item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDB(IdentityRole item)
        {
            throw new NotImplementedException();
        }
    }
}
