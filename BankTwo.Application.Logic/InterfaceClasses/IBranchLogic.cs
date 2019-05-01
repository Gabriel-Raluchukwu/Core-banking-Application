using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface IBranchLogic
    {
        Task<bool> AddNewBranch(BranchViewModel branchViewModel);

        IEnumerable<BranchViewModel> PopulateBranchDropDownList();

        IEnumerable<BranchViewModel> BranchDisplay();
    }
}
