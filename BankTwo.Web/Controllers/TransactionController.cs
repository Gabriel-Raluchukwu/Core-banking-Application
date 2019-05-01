using BankTwo.Application.Logic.InterfaceClasses;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{
    [Authorize(Roles = Roles.SuperAdministrator + "," + Roles.Administrator + "," + Roles.Teller)]
    public class TransactionController : AlertController
    {
        ITransactionLogic _transactionLogic;
        IEODLogic _EODLogic;
        public TransactionController(ITransactionLogic transactionLogic,IEODLogic eODLogic)
        {
            _transactionLogic = transactionLogic;
            _EODLogic = eODLogic;
        }
        
        public ActionResult PostTransaction()
        {

            TransactionViewModel transactionViewModel = new TransactionViewModel
            {
                GLAccountDropDown = _transactionLogic.PopulateDropDown()
            };
            return View(transactionViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostTransaction(TransactionViewModel transactionViewModel)
        {
            if (_EODLogic.IsBusinessClosed())
            {
                return View("~/Views/Notification_Views/BusinessLogic_Closed.cshtml");
            }

            if (ModelState.IsValid)
            {
                if (transactionViewModel.DebitAmount == transactionViewModel.CreditAmount && transactionViewModel.DebitAmount > 0)
                {
                    bool accountPostCheck = _transactionLogic.PostToGLAccount(transactionViewModel);
                    if (accountPostCheck)
                    {
                        bool check = await _transactionLogic.AddPosting(transactionViewModel);
                        if (check)
                        {
                            Success("Transaction Successful", true);
                            return RedirectToAction("DisplayTransactions");
                        }
                        Danger("Something Went wrong. Please try againTransaction Failed", true);
                    }
                    //return View("~/Views/Notification_Views/TransactionPost_Success.cshtml");
                    Success("Transaction Successful", true);
                    return RedirectToAction("DisplayTransactions");
                }
                //Display Notification
                Danger(" Debit Amount and Credit Amount must be equal", true);
            }
            transactionViewModel.GLAccountDropDown = _transactionLogic.PopulateDropDown();
            return View(transactionViewModel);
        }

        public ActionResult DisplayTransactions()
        {
            var transactions = _transactionLogic.ViewPostings();
            return View(transactions);
        }
    }
}