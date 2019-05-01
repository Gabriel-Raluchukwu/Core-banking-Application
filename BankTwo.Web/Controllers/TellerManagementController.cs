using BankTwo.Application.Logic;
using BankTwo.Application.Logic.InterfaceClasses;
using BankTwo.Application.Logic.Utilities;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{
    [Authorize(Roles = Roles.SuperAdministrator + " , " + Roles.Administrator)]
    public class TellerManagementController : AlertController
    {
        ITellerManagement _tellerManagement;
        IGLAccountLogic _gLAccountLogic;
       
        public TellerManagementController(ITellerManagement tellerManagement,
            IGLAccountLogic accountLogic)
        {
            _tellerManagement = tellerManagement;
            _gLAccountLogic = accountLogic;
        }
        public ActionResult AssignTillToUser(string id)
        {
            TellerRoleViewModel tellerViewModel;
            if (id != null)
            {
                tellerViewModel = new TellerRoleViewModel
                {
                    UserId = Encrypt.Decode(id),
                    GLAccounts = _tellerManagement.RetrieveNonTillGLAccount()
                };
                return View(tellerViewModel);
            }    
            
            tellerViewModel = new TellerRoleViewModel
            {
                GLAccounts = _tellerManagement.RetrieveNonTillGLAccount(),
            };
            return View(tellerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTillToUser(TellerRoleViewModel tellerViewModel)
        {    
           bool check = await _gLAccountLogic.MakeTill(tellerViewModel.GLAccountId);
            UserAuthentication userAuthentication = new UserAuthentication(Request.GetOwinContext().GetUserManager<ApplicationUserManager>(),
                Request.GetOwinContext().Get<ApplicationSignInManager>());
            if (check)
            {
                await userAuthentication.ChangeRoleToTeller(tellerViewModel);
                Success("Till Account assigned successfully",true);
                return RedirectToAction("DisplayUser","Account");
            }

            Danger("Something Went wrong. Please try again", true);
            return View(tellerViewModel);
        }
    }
}