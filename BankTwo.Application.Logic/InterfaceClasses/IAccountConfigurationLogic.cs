using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface IAccountConfigurationLogic
    {
        Task<bool> editSavingsConfiguration(ConfigSavingsViewModel savingsViewModel);

        Task<bool> editCurrentConfiguration(ConfigCurrentViewModel currentViewModel);

        Task<bool> editLoanConfiguration(ConfigLoanViewModel loanViewModel);

        ConfigSavingsViewModel RetrieveSavingsConfiguration();

        ConfigCurrentViewModel RetrieveCurrentConfiguration();

        ConfigLoanViewModel RetrieveLoanConfiguration();
     }  
}
