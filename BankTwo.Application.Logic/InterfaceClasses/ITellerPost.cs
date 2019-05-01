using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface ITellerPost
    {
        Task<bool> SaveTellerPosting(TellerPostViewModel tellerPostViewModel);

        bool TellerPosting(TellerPostViewModel tellerViewModel);

        IEnumerable<TellerPostViewModel> DisplayTellerPostings();
    }
}
