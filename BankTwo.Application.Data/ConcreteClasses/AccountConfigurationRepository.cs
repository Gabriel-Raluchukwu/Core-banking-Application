using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class AccountConfigurationRepository : IAccountConfigurationRepository
    {
        private static ApplicationDbContext dbContext;
        private static DatabasePersistence databasePersistence;
        public AccountConfigurationRepository()
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

        //Retrieve Configuration Methods
        public AccountConfiguration_Current RetrieveCurrentConfiguration()
        {
            AccountConfiguration_Current currentConfig;
            try
            {
                currentConfig = dbContext.CurrentConfiguration.SingleOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message,ex.InnerException,ex.StackTrace);
                return null;
            }
            return currentConfig;
           
        }

        public AccountConfiguration_Loan RetrieveLoanConfiguration()
        {
            AccountConfiguration_Loan loanConfig;
            try
            {
                loanConfig = dbContext.LoanConfiguration.SingleOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return loanConfig;
        }


        public AccountConfiguration_Savings RetrieveSavingsConfiguration()
        {
            AccountConfiguration_Savings savingConfig;
            try
            {
                savingConfig = dbContext.SavingsConfiguration.SingleOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
            return savingConfig;
        }

        //Update Configuration Methods
        public async Task<bool> UpdateCurrentConfiguration(AccountConfiguration_Current currentConfig)
        {
            dbContext.Entry(currentConfig).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }

        public async Task<bool> UpdateLoanConfiguration(AccountConfiguration_Loan loanConfig)
        {
            dbContext.Entry(loanConfig).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }

        public async Task<bool> UpdateSavingsConfiguration(AccountConfiguration_Savings savingsConfig)
        {
            dbContext.Entry(savingsConfig).State = EntityState.Modified;
            return await databasePersistence.PersistToDatabase();
        }
    }
}
