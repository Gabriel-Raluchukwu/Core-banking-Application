using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System.Threading.Tasks;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class AccountConfigurationLogic:IAccountConfigurationLogic
    {
        //private const int SavingsConfigurationId = 1001;
        //private const int CurrentConfigurationId = 2001;
        //private const int LoanConfigurationId = 3001;

        IAccountConfigurationRepository accountConfigurationRepository;
        public AccountConfigurationLogic(IAccountConfigurationRepository configurationRepository)
        {
            accountConfigurationRepository = configurationRepository;
        }
        public async Task<bool> editSavingsConfiguration(ConfigSavingsViewModel savingsViewModel)
        {
            var savingsConfiguration = accountConfigurationRepository.RetrieveSavingsConfiguration();
            if (savingsConfiguration != null)
            {
                //Manual Mapping
                savingsConfiguration.SavingsCreditInterestRate = savingsViewModel.SavingsCreditInterestRate;
                savingsConfiguration.SavingsMinimumBalance = savingsViewModel.SavingsMinimumBalance;
                savingsConfiguration.IEAccountId = savingsViewModel.IEAccountId;
                savingsConfiguration.DateLastUpdated = savingsViewModel.DateLastUpdated;

                bool check = await accountConfigurationRepository.UpdateSavingsConfiguration(savingsConfiguration);
                return check;
            }
            return false;
        }   
        
        public async Task<bool> editCurrentConfiguration(ConfigCurrentViewModel currentViewModel)
        {
            var currentConfiguration = accountConfigurationRepository.RetrieveCurrentConfiguration();
            if (currentConfiguration != null)
            {
                //Manual Mpping
                currentConfiguration.CurrentCreditInterestRate = currentViewModel.CurrentCreditInterestRate;
                currentConfiguration.CurrentMinimumBalance = currentViewModel.CurrentMinimumBalance;
                currentConfiguration.COT = currentViewModel.COT;
                currentConfiguration.COTIncomeId = currentViewModel.COTIncomeId;
                currentConfiguration.IEAccountId = currentViewModel.IEAccountId;
                currentConfiguration.DateLastUpdated = currentViewModel.DateLastUpdated;

                bool check = await accountConfigurationRepository.UpdateCurrentConfiguration(currentConfiguration);
                return check;
            }
            return false;
        }
        public async Task<bool> editLoanConfiguration(ConfigLoanViewModel loanViewModel)
        {
            var loanConfiguration = accountConfigurationRepository.RetrieveLoanConfiguration();
            if (loanConfiguration != null)
            {
                //Manual Mapping
                loanConfiguration.DebitInterestRate = loanViewModel.DebitInterestRate;
                loanConfiguration.InterestIncomeId = loanViewModel.InterestIncomeId;
                loanConfiguration.DateLastUpdated = loanViewModel.DateLastUpdated;

                bool check = await accountConfigurationRepository.UpdateLoanConfiguration(loanConfiguration);
                return check;
            }
            return false;
        }

        public ConfigSavingsViewModel RetrieveSavingsConfiguration()
        {
           var savingsConfig = accountConfigurationRepository.RetrieveSavingsConfiguration();
            return Mapper.Map<AccountConfiguration_Savings, ConfigSavingsViewModel>(savingsConfig);
        }
        public ConfigCurrentViewModel RetrieveCurrentConfiguration()
        {
            var currentConfig = accountConfigurationRepository.RetrieveCurrentConfiguration();
            return Mapper.Map<AccountConfiguration_Current, ConfigCurrentViewModel>(currentConfig);
        }
        public ConfigLoanViewModel RetrieveLoanConfiguration()
        {
            var currentConfig = accountConfigurationRepository.RetrieveLoanConfiguration();
            return Mapper.Map<AccountConfiguration_Loan,ConfigLoanViewModel>(currentConfig);
        }
    }
}
