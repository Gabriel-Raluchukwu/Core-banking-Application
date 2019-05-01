using BankTwo.Application.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface IGLAccountLogic
    {
        Task<bool> AddAccount(GLAccountViewModel gLAccountViewModel);

        IEnumerable<GLAccountDisplayViewModel> GLAccountDisplay();
   
        GLAccountViewModel RetrieveAccountById(string id);

        GLAccount RetrieveGLAccount(int id);

        Task<bool> EditAccount(GLAccountViewModel gLAccountViewModel);

        Task<bool> UpdateAccountBalance(GLAccount gLAccount);

        Task<bool> DeleteAccount(string id);

        IEnumerable<GLAccountDropDownViewModel> populateGLAccountDropDown();

        //Helper Methods
        Task<bool> MakeTill(int id);

        Task<bool> MakeVault(int id);

        GLAccountDropDownViewModel RetrieveVaultAccount();

        Task<bool> ResetVault();

        IEnumerable<BranchViewModel> PopulateBranchDropDown();

        IEnumerable<GLCategoryDropDownViewModel> PopulateCategoryDropDown();
       
    }
}
