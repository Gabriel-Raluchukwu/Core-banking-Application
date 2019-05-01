using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.InterfaceClasses
{
    public interface IAccountConfigurationRepository
    {
        //Savings Congiguration Methods
        AccountConfiguration_Savings RetrieveSavingsConfiguration();

        Task<bool> UpdateSavingsConfiguration(AccountConfiguration_Savings savingConfig);

        //Current Cofiguration Methods
        AccountConfiguration_Current RetrieveCurrentConfiguration();

        Task<bool> UpdateCurrentConfiguration(AccountConfiguration_Current currentConfig);

        //Loan Congiguration Methods
        AccountConfiguration_Loan RetrieveLoanConfiguration();

        Task<bool> UpdateLoanConfiguration(AccountConfiguration_Loan loanConfig);

    }
}
