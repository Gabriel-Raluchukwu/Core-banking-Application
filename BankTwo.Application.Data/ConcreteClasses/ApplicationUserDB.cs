using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class ApplicationUserDB:IApplicationUser
    {
        //ApplicationUser
        
        public const bool Failure = false;
        public const bool Success = true;

        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public ApplicationUserDB()
        {
            if (dbContext == null)
            {
                dbContext = new ApplicationDbContext();
            }
            databasePersistence = new DatabasePersistence(dbContext);
        }

        public async Task<bool> DeleteFromDB(string id)
        {
            
            var deleteUser = dbContext.Users.Find(id);
            if (deleteUser == null)
            {
                return Failure;
            }
            dbContext.Users.Remove(deleteUser);
            return await databasePersistence.PersistToDatabase();
        }

        public async Task<bool> InsertToDB(ApplicationUser user)
        {
            dbContext.Users.Add(user);
            return await databasePersistence.PersistToDatabase();
        }

       
        public IEnumerable<ApplicationUser> RetrieveAllFromDB()
        {
            IEnumerable<ApplicationUser> users;
            try
            {
                users = dbContext.Users.Include(b => b.Branch).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            
            return users;
        }
   
        public ApplicationUser RetrieveById(string id)
        {
            ApplicationUser user = null;
            try
            {
                 user = dbContext.Users.SingleOrDefault(i => i.Id == id);
               
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
            }
            return user;
        }

        public async Task<bool> UpdateDB(ApplicationUser user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }

        public string RetrieveRoleName(string id)
        {
            var oldRoleName = dbContext.Roles.SingleOrDefault(r => r.Id == id).Name;
            return oldRoleName;
        }
        public List<String> RetrieveUserNames()
        {
            var userNames = from Users in dbContext.Users
                            select new { Users.UserName};
            List<string> Names = new List<string>();
            foreach (var row in userNames)
            {
                Names.Add(row.UserName.ToLower());
            }
            return Names;
        }
    }
}
