using System.Collections.Generic;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface ITellerManagement
    {     
        IEnumerable<GLAccountDropDownViewModel> RetrieveNonTillGLAccount();

        IEnumerable<GLAccountDropDownViewModel> RetrieveTillGLAccount();
    }
}
