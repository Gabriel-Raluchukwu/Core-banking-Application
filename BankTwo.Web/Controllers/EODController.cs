using BankTwo.Application.Logic.InterfaceClasses;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankTwo.Web.Controllers
{
    [Authorize(Roles = Roles.SuperAdministrator)]
    public class EODController : AlertController
    {
        IEODLogic _eODLogic;
        public EODController(IEODLogic logic)
        {
            _eODLogic = logic;     
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OpenBusiness()
        {
            if (!_eODLogic.IsBusinessClosed())
            {
                Success("Business is Open",true);
                return RedirectToAction("Index");
            }
            var result = Task.Run(()=> _eODLogic.OpenBusiness());
            bool check = result.Result;
            if (check)
            {
                return View("~/Views/Notification_Views/BusinessLogic_Open.cshtml");
            }

            Danger("Something Went wrong. Please try again", true);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> CloseBusiness()
        {
            var result = await _eODLogic.CloseBusiness();
            //bool check = result.Result;
            if (result)
            {
                return View("~/Views/Notification_Views/BusinessLogic_Closed.cshtml");
            }

            Danger("Something Went wrong. Please try again", true);
            return RedirectToAction("Index");
        }
    }
}