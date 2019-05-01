using BankTwo.Application.Logic.Utilities;
using BankTwo.Application.Logic.InterfaceClasses;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using ViewModels;
using BankTwo.Application.Logic;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace BankTwo.Web.Controllers
{
    
    public class TellerPostingController : AlertController
    {
        IGLAccountLogic _gLAccountLogic;
        ITellerPost _tellerPost;
        ITellerManagement _tellerManagement;
        IEODLogic _EODLogic;
        public TellerPostingController(IGLAccountLogic gLAccount,ITellerPost tellerPosting,
            ITellerManagement teller,IEODLogic eODLogic)
        {
            _EODLogic = eODLogic;
            _tellerManagement = teller;
            _gLAccountLogic = gLAccount;
            _tellerPost = tellerPosting;
        }
        [Authorize(Roles = Roles.SuperAdministrator + " , " + Roles.Teller)]
        public ActionResult PostTeller( string id)
        {
            TellerPostViewModel tellerViewModel;
            if (id != null)
            {
                 tellerViewModel = new TellerPostViewModel
                {
                    CustomerAccountId = int.Parse(Encrypt.Decode(id)),
                    GLAccounts = _tellerManagement.RetrieveTillGLAccount()
                };
                return View(tellerViewModel);
            }
             tellerViewModel = new TellerPostViewModel()
            {
                GLAccounts = _tellerManagement.RetrieveTillGLAccount()
            };
            return View(tellerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostTeller(TellerPostViewModel tellerViewModel)
        {
            if (tellerViewModel.CustomerAccountId == 0)
            {
                Danger("Please Select a customer", true);
                tellerViewModel.GLAccounts = _tellerManagement.RetrieveTillGLAccount();
                return View(tellerViewModel);
            }
            UserAuthentication userAuthentication = new UserAuthentication(Request.GetOwinContext().GetUserManager<ApplicationUserManager>(),
                Request.GetOwinContext().Get<ApplicationSignInManager>());
            if (_EODLogic.IsBusinessClosed())
            {
                return View("~/Views/Notification_Views/BusinessLogic_Closed.cshtml");
            }

            if (userAuthentication.RetrieveUser(User.Identity.GetUserId()).GLAccountId.HasValue)
            {
                tellerViewModel.GLAccountId = userAuthentication.RetrieveUser(User.Identity.GetUserId()).GLAccountId.Value;
                tellerViewModel.TransactionDate = _EODLogic.RetrieveFinancialDate();
                if (ModelState.IsValid)
                {
                    bool check = _tellerPost.TellerPosting(tellerViewModel);
                    if (check)
                    {
                        _tellerPost.SaveTellerPosting(tellerViewModel);
                        return View("~/Views/Notification_Views/TransactionPost_Success.cshtml");
                    }
                    Danger(string.Format("Error occured during Transaction Post.Ensure Till account and Customers account have enough funds.", User.Identity.GetUserName()), true);
                }
            }
            else
            {
                Danger(string.Format("No Till Account Found. Assign Till Account to user",User.Identity.GetUserName()),true);
            }
           
            return View(tellerViewModel);
        }
        [Authorize(Roles = Roles.SuperAdministrator + " , " + Roles.Teller + " , " + Roles.CustomerCare)]
        public ActionResult DisplayTellerPostings()
        {
            var tellerPostings = _tellerPost.DisplayTellerPostings();
            return View(tellerPostings);
        }
    }
}