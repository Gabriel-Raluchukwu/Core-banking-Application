using BankTwo.Application.Logic.InterfaceClasses;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{
    [Authorize()]
    public class AccountConfigurationController : AlertController
    {
        IAccountConfigurationLogic accountConfigurationLogic;
        IGLAccountLogic gLAccountLogic;
        public AccountConfigurationController(IAccountConfigurationLogic accountConfigLogic,
            IGLAccountLogic accountLogic)
        {
            accountConfigurationLogic = accountConfigLogic;
            gLAccountLogic = accountLogic;
        }
        public ActionResult EditSavingsConfiguration()
        {
            var configSavings = accountConfigurationLogic.RetrieveSavingsConfiguration();
            configSavings.GLAccounts = gLAccountLogic.populateGLAccountDropDown();
            
            return View(configSavings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSavingsConfiguration(ConfigSavingsViewModel savingsConfigurationViewModel)
        {
            savingsConfigurationViewModel.DateLastUpdated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                savingsConfigurationViewModel.GLAccounts = gLAccountLogic.populateGLAccountDropDown();
                return View(savingsConfigurationViewModel);
            }

           bool check = await accountConfigurationLogic.editSavingsConfiguration(savingsConfigurationViewModel);
            if (check)
            {
                Success("Savings Account Configuration saved Successfully",true);
                return RedirectToAction(actionName: "DisplayCustomerAccounts", controllerName: "CustomerAccount");
            }
            Danger(" !Something Went Wrong. Please Try again",true);
            return View(savingsConfigurationViewModel);
        }

        public ActionResult EditCurrentConfiguration()
        {
            var configCurrent = accountConfigurationLogic.RetrieveCurrentConfiguration();
            configCurrent.GLAccounts = gLAccountLogic.populateGLAccountDropDown();
            return View(configCurrent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCurrentConfiguration(ConfigCurrentViewModel currentConfigurationViewModel)
        {
            currentConfigurationViewModel.DateLastUpdated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                currentConfigurationViewModel.GLAccounts = gLAccountLogic.populateGLAccountDropDown();
                return View(currentConfigurationViewModel);
            }
            
            bool check = await accountConfigurationLogic.editCurrentConfiguration(currentConfigurationViewModel);
            if (check)
            {
                Success("Current Account Configuration saved Successfully",true);
                return RedirectToAction(actionName: "DisplayCustomerAccounts", controllerName: "CustomerAccount");
            }
            return View(currentConfigurationViewModel);
        }

        public ActionResult EditLoanConfiguration()
        {
            var configLoan = accountConfigurationLogic.RetrieveLoanConfiguration();
            configLoan.GLAccounts = gLAccountLogic.populateGLAccountDropDown();
            return View(configLoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLoanConfiguration(ConfigLoanViewModel loanConfigurationViewModel)
        {
            loanConfigurationViewModel.DateLastUpdated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                loanConfigurationViewModel.GLAccounts = gLAccountLogic.populateGLAccountDropDown();
                return View(loanConfigurationViewModel);
            }
            bool check = await accountConfigurationLogic.editLoanConfiguration(loanConfigurationViewModel);
            if (check)
            {
                Success(" Loan Account Configuration saved Successfully", true);
                return RedirectToAction(actionName: "DisplayCustomerAccounts", controllerName: "CustomerAccount");
            }
            return View(loanConfigurationViewModel);
        }

    }
    
}