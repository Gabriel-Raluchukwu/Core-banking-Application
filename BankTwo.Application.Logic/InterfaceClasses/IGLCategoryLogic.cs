using BankTwo.Application.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface IGLCategoryLogic
    {
        Task<bool> AddCategory(GLCategoryViewModel gLCategoryViewModel);

        IEnumerable<GLCategoryDisplayViewModel> GLCategoryDisplay();

        GLCategoryViewModel RetrieveCategoryById(string id);

        GLCategory RetrieveGLCategory(int id);

        Task<bool> EditCategory(GLCategoryViewModel gLCategoryViewModel);


        Task<bool> DeleteCategory(string id);

        //
        //Helper Methods
        int GetMainAccountId(int categoryId);

        IEnumerable<GLCategoryDropDownViewModel> PopulateCategoryDropDown();
    }
}
