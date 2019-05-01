using BankTwo.Application.Logic.InterfaceClasses;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{
   // [Authorize(Roles = Roles.Administrator + "," + Roles.Administrator)]
    public class GLCategoryController : AlertController
    {
        IGLCategoryLogic _gLCategoryLogic;
        public GLCategoryController(IGLCategoryLogic gLCategoryLogic)
        {
            this._gLCategoryLogic = gLCategoryLogic;
        }
        public ActionResult CreateCategory()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(GLCategoryViewModel gLCategoryViewModel)
        {
            DateTime currentDateTime = DateTime.Now;
            gLCategoryViewModel.DateAdded = currentDateTime;
            gLCategoryViewModel.DateLastUpdated = currentDateTime;
            if (ModelState.IsValid)
            {
               Task<bool> isFinished =Task.Run(() => _gLCategoryLogic.AddCategory(gLCategoryViewModel));
                bool check = isFinished.Result;
                if (check)
                {
                    Success(string.Format("{0} saved Successfully", gLCategoryViewModel.GLCategoryName), true);
                    return RedirectToAction("DisplayCategories");
                }
                
            }
            Danger("Something Went wrong. Please try again", true);
            return View(gLCategoryViewModel);
        }

        public ActionResult DisplayCategories()
        {
            var glCategories = _gLCategoryLogic.GLCategoryDisplay();
            return View(glCategories);
        }

        public ActionResult EditCategory(string id)
        {
            var glCategory = _gLCategoryLogic.RetrieveCategoryById(id);
            GLCategoryDisplayViewModel glCategoryEdit = new GLCategoryDisplayViewModel
            {
                EncryptedId = glCategory.EncryptedId,
                GLCategoryName = glCategory.GLCategoryName,
                GLCategoryDescription = glCategory.GLCategoryDescription             
            };
            return View(glCategoryEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(GLCategoryDisplayViewModel gLCategoryDisplayViewModel)
        {
            if (ModelState.IsValid)
            {
                GLCategoryViewModel gLCategoryViewModel = new GLCategoryViewModel
                {
                    EncryptedId = gLCategoryDisplayViewModel.EncryptedId,
                    GLCategoryName = gLCategoryDisplayViewModel.GLCategoryName,
                    GLCategoryDescription = gLCategoryDisplayViewModel.GLCategoryDescription,
                    DateLastUpdated = DateTime.Now
                };
                bool check = await _gLCategoryLogic.EditCategory(gLCategoryViewModel);
                if (check)
                {
                    Success(string.Format("{0} edit Unccessful", gLCategoryDisplayViewModel.GLCategoryName), true);
                    return RedirectToAction(actionName: "DisplayCategories");
                }
            }

            //Display Notification
            Danger(string.Format("{0} edit Unsuccessful", gLCategoryDisplayViewModel.GLCategoryName), true);
            return View(gLCategoryDisplayViewModel);
        }

        public async Task<ActionResult> DeleteCategory(string id)
        {
            bool check = await _gLCategoryLogic.DeleteCategory(id);
            if (check)
            {
                //Display Notification
                Success("Delete successful", true);
                return RedirectToAction(actionName: "DisplayCategories");
            }
            //Display Notification
            Danger("Something Went wrong. Please try again", true);
            return RedirectToAction(actionName: "DisplayCategories");
        }
    }
}