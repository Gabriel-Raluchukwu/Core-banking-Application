using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface ILoanAccountLogic
    {
        Task<bool> SaveLoanAccount(LoanAccountViewModel loanAccountViewModel);

        IEnumerable<LoanAccountDisplayViewModel> RetrieveAllAccounts();

        Task<bool> EditLoanAccount(LoanAccountViewModel loanAccountViewModel);

        LoanAccountViewModel RetrieveLoanAccountById(string id);

        Task<bool> CloseAccount(string id);

    }
}
