using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;
using System.Collections.Generic;
using BankTwo.Application.Logic;

namespace BankTwo.Web.Controllers
{

    public class AccountController : AlertController
    {
       
        UserAuthentication userAuthentication;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private IBranchLogic _branchLogic;
        private IRoleLogic _roleLogic;


        public AccountController(IBranchLogic branchLogic,IRoleLogic roleLogic)
        {
            _branchLogic = branchLogic;
            _roleLogic = roleLogic;
            //userAuthentication = new UserAuthentication(userManager, signInManager);
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;          
        }


        [Authorize(Roles = Roles.Administrator + " , " + Roles.SuperAdministrator)]
        public ActionResult Register()
        {
           
            RegisterViewModel registerViewModel = new RegisterViewModel
            {
                BranchList = _branchLogic.PopulateBranchDropDownList(),
                IdentityRoles = _roleLogic.PopulateRolesDropDownList() 
            };
            return View(registerViewModel);
        }

    
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = Roles.Administrator + " , " + Roles.SuperAdministrator)]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }

            if (ModelState.IsValid)
            {
                var UserNames = userAuthentication.RetrieveUserNames();

                if (!UserNames.Contains(model.UserName.ToLower()))
                {
                    var result = await userAuthentication.RegisterLogic(model);
                    if (result.Succeeded)
                    {

                        // await userAuthentication.SignInUser(model.UserRole);

                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        Success(string.Format("{0} {1} saved Successfully", model.LastName, model.FirstName), true);
                        return RedirectToAction("DisplayUser", "Account");
                    }
                    AddErrors(result);
                }
                Danger(string.Format("{0} user name already exits",model.UserName),true);
            }

            // If we got this far, something failed, redisplay form
            Danger("Insert Operation Failed", true);
            RegisterViewModel registerViewModel = new RegisterViewModel
            {
                BranchList = _branchLogic.PopulateBranchDropDownList(),
                IdentityRoles = _roleLogic.PopulateRolesDropDownList()
            };
            return View(registerViewModel);
    
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager,_signInManager);
            }
            var result = await userAuthentication.LogInLogic(model);
            switch (result)
            {
                case SignInStatus.Success:
                    // change to UserName
                    Success(string.Format("Welcome {0} ",model.UserName),true);
                    return RedirectToAction("DisplayCustomers","Customer");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }
            userAuthentication.LogOff();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult DisplayUser()
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }

            var userModels = userAuthentication.UsersDisplay();
            
            return View(userModels);
        }

        public ActionResult SelectUserAccount()
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }

            var userModels = userAuthentication.UsersDisplay();

            return View(userModels);
        }

        [Authorize]
        public ActionResult EditUser(string id)
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }
            RegisterViewModel registerViewModel = userAuthentication.RetrieveUserById(id);
            if (registerViewModel == null)
            {
                //DisplayNotification
                
            }
            registerViewModel.BranchList = _branchLogic.PopulateBranchDropDownList();
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> EditUser(RegisterViewModel registerViewModel)
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }
           bool check = await userAuthentication.EditUser(registerViewModel);
            if (check)
            {
                //Display Notification
                Success(string.Format("{0} {1} edit Successfull",registerViewModel.LastName,registerViewModel.FirstName),true);
                return RedirectToAction(actionName: "DisplayUser");
            }
            //Display Notification
            Danger("Edit Operation Failed. Please try Again",true);
            return RedirectToAction(actionName:"DisplayUser");
        }
        [Authorize(Roles = Roles.Administrator + " , " + Roles.SuperAdministrator)]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }
            bool check = await userAuthentication.DeleteUser(id);
            if (check)
            {
                //Display Notification
                Success("Delete successfull", true);
                return RedirectToAction(actionName:"DisplayUser");
            }
            //Display Notification
            Danger("Oops! Something Went wrong. Please try again",true);
            return RedirectToAction(actionName: "DisplayUser");
        }

        public JsonResult IsUserNameAvailable(string UserLogInName)
        {
            if (userAuthentication == null)
            {
                userAuthentication = new UserAuthentication(_userManager, _signInManager);
            }
            List<string> userNames = userAuthentication.RetrieveUserNames();
            if (userNames.Contains(UserLogInName.ToLower()))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
           
        }
        //protected override void Dispose(bool disposing)
        //{
        //    userAuthentication.Dispose(disposing);
        //    base.Dispose(disposing);
        //}

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        
        #endregion
    }
}