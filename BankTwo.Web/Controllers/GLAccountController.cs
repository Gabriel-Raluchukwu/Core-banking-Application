using BankTwo.Application.Logic.InterfaceClasses;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{

    public class GLAccountController : AlertController
    {
        private IGLAccountLogic _gLAccountLogic;
        public GLAccountController(IGLAccountLogic gLAccountLogic)
        {
            _gLAccountLogic = gLAccountLogic;
        }

        public ActionResult CreateGLAccount()
        {
            GLAccountViewModel gLAccountViewModel = new GLAccountViewModel
            {
                Branches = _gLAccountLogic.PopulateBranchDropDown(),
                GLCategories = _gLAccountLogic.PopulateCategoryDropDown()
            };
            return View(gLAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.SuperAdministrator + "," + Roles.Administrator)]
        public ActionResult CreateGLAccount(GLAccountViewModel gLAccountViewModel)
            {
            DateTime currentDateTime = DateTime.Now;
            gLAccountViewModel.DateAdded = currentDateTime;
            gLAccountViewModel.DateLastUpdated = currentDateTime;
            if (ModelState.IsValid)
            {
                Task<bool> isFinished = Task.Run(() => _gLAccountLogic.AddAccount(gLAccountViewModel));
                bool check = isFinished.Result;
                if (check)
                {
                    Success(string.Format("{0} saved Successfully",gLAccountViewModel.GLAccountName),true);
                    return RedirectToAction("DisplayGLAccounts");
                }
                
            }
            Danger("Something Went wrong. Please try again", true);
            return View(gLAccountViewModel);
        }

        public ActionResult DisplayGLAccounts()
        {
            var glAccounts = _gLAccountLogic.GLAccountDisplay();
            return View(glAccounts);
        }
        [HttpGet]
        [Authorize(Roles = Roles.SuperAdministrator + "," + Roles.Administrator)]
        public ActionResult EditGLAccount(string id)
        {
            var glAccount = _gLAccountLogic.RetrieveAccountById(id);
            GLAccountDisplayViewModel gLAccountDisplayViewModel = new GLAccountDisplayViewModel
            {
                EncryptedId = glAccount.EncryptedId,
                GLAccountName = glAccount.GLAccountName,
                Branches = _gLAccountLogic.PopulateBranchDropDown()
            };
            return View(gLAccountDisplayViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.SuperAdministrator + "," + Roles.Administrator)]
        public async Task<ActionResult> EditGLAccount(GLAccountDisplayViewModel gLAccountDisplayViewModel)
        {
            if (ModelState.IsValid)
            {
                GLAccountViewModel gLAccountViewModel = new GLAccountViewModel
                {
                    EncryptedId = gLAccountDisplayViewModel.EncryptedId,
                    GLAccountName = gLAccountDisplayViewModel.GLAccountName,
                    BranchId = gLAccountDisplayViewModel.BranchId,
                    DateLastUpdated = DateTime.Now
                };
                bool check = await _gLAccountLogic.EditAccount(gLAccountViewModel);
                if (check)
                {
                    //Display Notification
                    Success(string.Format("{0} Edit Successful", gLAccountViewModel.GLAccountName), true);
                    return RedirectToAction(actionName: "DisplayGLAccounts");
                }
            }

            //Display Notification
            Danger(string.Format("{0} edit Unsuccessful",gLAccountDisplayViewModel.GLAccountName), true);
            return View(gLAccountDisplayViewModel);
        }

        public async Task<ActionResult> DeleteGLAccount(string id)
        {
            bool check = await _gLAccountLogic.DeleteAccount(id);
            if (check)
            {
                //Display Notification
                Success("Delete successful", true);
                return RedirectToAction(actionName: "DisplayGLAccounts");
            }
            //Display Notification
            Danger("Something Went wrong. Please try again", true);
            return RedirectToAction(actionName: "DisplayGLAccounts");
        }
    }
}