using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;
using BankTwo.Application.Logic.Utilities;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class GLCategoryLogic:IGLCategoryLogic
    {
        private IGLCategoryRepository<GLCategory> gLCategoryRepository;
        public GLCategoryLogic(IGLCategoryRepository<GLCategory> gLCategory)
        {
            this.gLCategoryRepository = gLCategory;
        }
        public async Task<bool> AddCategory(GLCategoryViewModel gLCategoryViewModel)
        {
            gLCategoryViewModel.MainCategoriesId = (int)gLCategoryViewModel.MainCategoryEnum;
            var glCategory = Mapper.Map<GLCategoryViewModel, GLCategory>(gLCategoryViewModel);
            return await gLCategoryRepository.InsertToDB(glCategory);
           
        }
        public IEnumerable<GLCategoryDisplayViewModel> GLCategoryDisplay()
        {
            IEnumerable<GLCategoryDisplayViewModel> gLCategoryDisplayViewModels;
            var categories = gLCategoryRepository.RetrieveAllFromDB();
            if (categories != null)
            {
                gLCategoryDisplayViewModels = categories.Select(Mapper.Map<GLCategory, GLCategoryDisplayViewModel>).ToList();
                return gLCategoryDisplayViewModels;
            }
            return null;
        }
        public GLCategoryViewModel RetrieveCategoryById(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            var retrievedCategory = gLCategoryRepository.RetrieveById(Id);
            if (retrievedCategory == null)
            {
                return null;
            }
            var categoryViewModel = Mapper.Map<GLCategory,GLCategoryViewModel>(retrievedCategory);
           
            return categoryViewModel;
        }
        public GLCategory RetrieveGLCategory(int id)
        {
            var retrievedCategory = gLCategoryRepository.RetrieveById(id);
            return retrievedCategory;
        }
        public async Task<bool> EditCategory(GLCategoryViewModel gLCategoryViewModel)
        {
            int id = int.Parse(Encrypt.Decode(gLCategoryViewModel.EncryptedId));
            var categoryToUpdate = gLCategoryRepository.RetrieveById(id);
            //Manual Mapping
            categoryToUpdate.GLCategoryName = gLCategoryViewModel.GLCategoryName;
            categoryToUpdate.GLCategoryDescription = gLCategoryViewModel.GLCategoryDescription;

            return await gLCategoryRepository.UpdateDB(categoryToUpdate);
        }

        public async Task<bool> DeleteCategory(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            return await gLCategoryRepository.DeleteFromDB(Id);
        }


        //
        //Helper Methods

        public int GetMainAccountId(int categoryId)
        {
            var retrievedCategory = gLCategoryRepository.RetrieveById(categoryId);
            return retrievedCategory.MainCategoriesId;
        }
   
        public IEnumerable<GLCategoryDropDownViewModel> PopulateCategoryDropDown()
        {
            var retrievedCategories = gLCategoryRepository.RetrieveAllFromDB();
            var glCategoryDropDownViewModels = retrievedCategories.Select(Mapper.Map<GLCategory, GLCategoryDropDownViewModel>);
            return glCategoryDropDownViewModels;
        }

       
    }
}
