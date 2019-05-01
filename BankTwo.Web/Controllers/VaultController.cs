using BankTwo.Application.Logic.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{
    [Authorize(Roles = Roles.SuperAdministrator)]
    public class VaultController : AlertController
    {
        ITransactionLogic _transactionLogic;
        IGLAccountLogic _gLAccountLogic;
        IEODLogic _EODLogic;
        public VaultController(IGLAccountLogic accountLogic,ITransactionLogic transaction,
            IEODLogic eODLogic)
        {
            _EODLogic = eODLogic;
            _transactionLogic = transaction;
            _gLAccountLogic = accountLogic;
        }

        public ActionResult SetVaultAccount()
        {
            //var glAccounts = _gLAccountLogic.GLAccountDisplay().W
            VaultViewModel vaultViewModel = new VaultViewModel
            {
                GLAccountDropDown = _gLAccountLogic.populateGLAccountDropDown()
            };
            TempData["Vault"] = "false";
            return View(vaultViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetVaultAccount(VaultViewModel vaultViewModel)
        {
            var gLAccounts = _gLAccountLogic.GLAccountDisplay().Where(m => m.IsVault == true);
            if (gLAccounts.Count() >= 1)
            {
                TempData["Vault"] = "true";
                vaultViewModel = new VaultViewModel
                {
                    GLAccountDropDown = _gLAccountLogic.populateGLAccountDropDown()
                };
                return View(vaultViewModel);
            }
            TempData["Vault"] = "false";
            Task<bool> isFinished = Task.Run(() => _gLAccountLogic.MakeVault(vaultViewModel.GLAccountId));
            bool finished = isFinished.Result;
            if (finished)
            {
                Success("Vault Account Set Successfully",true);
                return RedirectToAction("LoadVaultAccount");
            }
            Danger("Something Went wrong. Please try again", true);
            return View(vaultViewModel);
        }

        public ActionResult LoadVaultAccount()
        {
            var capitalGLAccounts = _gLAccountLogic.populateGLAccountDropDown().Where(a => a.GLAccountCode.ToString().Substring(0,1) == "3");
            
            VaultViewModel vaultViewModel = new VaultViewModel
            {
                GLAccountDropDown = capitalGLAccounts
            };
            return View(vaultViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadVaultAccount(VaultViewModel vaultViewModel)
        {
            if (_EODLogic.IsBusinessClosed())
            {
                return View("~/Views/Notification_Views/BusinessLogic_Closed.cshtml");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var vaultAccount = _gLAccountLogic.RetrieveVaultAccount();
            if (vaultAccount ==  null)
            {
                Danger("Vault Account Not Found",true);
            }
            TransactionViewModel loadVaultViewModel = new TransactionViewModel
            {
                DebitGLAccountId = vaultAccount.Id,
                DebitAmount = vaultViewModel.VaultAmount,
                CreditAmount = vaultViewModel.VaultAmount,
                CreditGLAccountId = vaultViewModel.GLAccountId,
                Narration = string.Format("loading Vault Account with {0}",vaultViewModel.VaultAmount)
            };
            _transactionLogic.PostToGLAccount(loadVaultViewModel);
            return View("~/Views/Notification_Views/LoadVault_Success.cshtml");
        }

        public ActionResult ResetVault()
        {
            _gLAccountLogic.ResetVault();
            return RedirectToAction("SetVaultAccount");
        }
    }
}