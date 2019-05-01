using BankTwo.Application.Logic.InterfaceClasses;
using BankTwo.Application.Logic.Utilities;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;


namespace BankTwo.Web.Controllers
{
    [Authorize]
    public class CustomerAccountController : AlertController
    {
        private ICustomerAccountLogic _customerAccountLogic;
        private ILoanAccountLogic _loanAccountLogic;
        public CustomerAccountController(ICustomerAccountLogic customerAccountLogic,ILoanAccountLogic loanAccount)
        {
            _customerAccountLogic = customerAccountLogic;
            _loanAccountLogic = loanAccount;
        }

        [HttpGet]
        [Authorize(Roles = Roles.SuperAdministrator + " , " + Roles.Teller + " , " + Roles.CustomerCare)]
        public ActionResult CreateCustomerAccount(string id)
        {
            CustomerAccountViewModel customerAccountViewModel;
            if (id != null)
            {
                 customerAccountViewModel = new CustomerAccountViewModel
                {
                    CustomerId = int.Parse(Encrypt.Decode(id)),
                    Branches = _customerAccountLogic.PopulateBranchDropDown()
                };
                return View(customerAccountViewModel);

            }
             customerAccountViewModel = new CustomerAccountViewModel
            {
                Branches = _customerAccountLogic.PopulateBranchDropDown()
            };
            return View(customerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomerAccount(CustomerAccountViewModel customerAccountViewModel)
        {
            if (customerAccountViewModel.CustomerId == 0)
            {
                Danger("Please Select a customer",true);
                customerAccountViewModel.Branches = _customerAccountLogic.PopulateBranchDropDown();
                return View(customerAccountViewModel);
            }
            DateTime currentDateTime = DateTime.Now;
            customerAccountViewModel.DateAdded = currentDateTime;
            customerAccountViewModel.DateLastUpdated = currentDateTime;
            customerAccountViewModel.CAccountNumber = AutoGenerator.GenerateCustomerAccountIdentificationNo(customerAccountViewModel.CustomerId, (int)customerAccountViewModel.AccountTypeEnum);
            if (ModelState.IsValid)
            {
                var check = await _customerAccountLogic.AddCustomerAccount(customerAccountViewModel);
                if (check)
                {
                    Success(string.Format("{0} Account saved Successfully",customerAccountViewModel.CAccountName),true);
                    return RedirectToAction("DisplayCustomerAccounts");
                }
               
            }
            Danger(string.Format("{0} save Unsuccessfull",customerAccountViewModel.CAccountName),true);
            customerAccountViewModel.Branches = _customerAccountLogic.PopulateBranchDropDown();
            return View(customerAccountViewModel);
        }

        public ActionResult DisplayCustomerAccounts()
        {
            var customerAccounts = _customerAccountLogic.CustomerAccountDisplay();
            return View(customerAccounts);
        }

        [Authorize(Roles = Roles.SuperAdministrator + " , " + Roles.Teller + " , " + Roles.CustomerCare)]
        public ActionResult EditCustomerAccount(string id)
        {
            var customerAccount = _customerAccountLogic.RetrieveCustomerAccountById(id);
            CustomerAccountViewModel customerAccountViewModel = new CustomerAccountViewModel
            {
                EncryptedId = customerAccount.EncryptedId,
                CAccountName = customerAccount.CAccountName,
                BranchId = customerAccount.BranchId,
                Branches = _customerAccountLogic.PopulateBranchDropDown()
            };
            return View(customerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomerAccount(CustomerAccountViewModel customerAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                bool check = await _customerAccountLogic.EditCustomerAccount(customerAccountViewModel);
                if (check)
                {
                    //Display Notification
                    Success(string.Format("{0} Edit Successful",customerAccountViewModel.CAccountName),true);
                    return RedirectToAction(actionName: "DisplayCustomerAccounts");
                }
            }

            //Display Notification
            Danger(string.Format("{0} edit Unsuccessful. Please try again",customerAccountViewModel.CAccountName),true);
            customerAccountViewModel.Branches = _customerAccountLogic.PopulateBranchDropDown();
            return View(customerAccountViewModel);
        }

        public async Task<ActionResult> DeleteCustomerAccount(string id)
        {
            bool check = await _customerAccountLogic.DeleteCustomerAccount(id);
            if (check)
            {
                //Display Notification
                Success("Delete successful", true);
                return RedirectToAction(actionName: "DisplayCustomerAccounts");
            }
            //Display Notification
            Danger("Something Went wrong. Please try again", true);
            return RedirectToAction(actionName: "DisplayCustomerAccounts");
        }
        public ActionResult SelectCustomerAccountForLoan()
        {
            var customerAccounts = _customerAccountLogic.CustomerAccountDisplay();
            return View(customerAccounts);
        }
        public ActionResult DisplayOpenAccounts()
        {
            var customerAccounts = _customerAccountLogic.RetrieveOpenCustomerAccount();
            return View(customerAccounts);
        }

        public ActionResult DisplayCustomerAccountsSelect()
        {
            var customerAccounts = _customerAccountLogic.CustomerAccountDisplay();
            return View(customerAccounts);
        }

        [Authorize(Roles = Roles.SuperAdministrator + " , " + Roles.Teller + " , " + Roles.CustomerCare)]
        public async Task<ActionResult> CloseAccount(string id)
        {
            bool closed = await _customerAccountLogic.CloseAccount(id);
            if (closed)
            {
                //Display Notification Account Closed
                Success("Account Closed Successfully", true);
                return RedirectToAction("DisplayCustomerAccounts");
            }
            //Display Notification
            Danger("Oops! Something Went wrong. Please try again", true);
            return RedirectToAction("DisplayOpenAccounts");
        }

        [Authorize(Roles = Roles.SuperAdministrator + " , " + Roles.Teller + " , " + Roles.CustomerCare)]
        public ActionResult AddLoanAccount(string id)
        {
            LoanAccountViewModel loanAccount;
            if (id != null)
            {
                loanAccount = new LoanAccountViewModel()
                {
                    CustomerAccountId = int.Parse(Encrypt.Decode(id)),
                     Branches = _customerAccountLogic.PopulateBranchDropDown()
                };
                return View(loanAccount);
            }
            loanAccount = new LoanAccountViewModel()
            {
                Branches = _customerAccountLogic.PopulateBranchDropDown()
            };
            return View(loanAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLoanAccount(LoanAccountViewModel loanAccountViewModel)
        {
            if (loanAccountViewModel.CustomerAccountId == 0)
            {
                Danger("Please Select a customer", true);
                loanAccountViewModel.Branches = _customerAccountLogic.PopulateBranchDropDown();
                return View(loanAccountViewModel);
            }
            if (ModelState.IsValid)
            {
                loanAccountViewModel.CAccountNumber = AutoGenerator.GenerateCustomerAccountIdentificationNo(loanAccountViewModel.CustomerAccountId, 3);
                loanAccountViewModel.LoanStartDate = new DateTime(loanAccountViewModel.Year,loanAccountViewModel.Month,loanAccountViewModel.Day);
                loanAccountViewModel.LoanDueDate = loanAccountViewModel.LoanStartDate.AddMonths(loanAccountViewModel.LoanDuration);
                loanAccountViewModel.LoanStats = LoanAccountViewModel.LoanStatus.LoanOngoing;
                Task<bool> isFinished = Task.Run(() => _loanAccountLogic.SaveLoanAccount(loanAccountViewModel));
                bool check = isFinished.Result;
                if (check)
                {
                    Success(string.Format("Loan Account saved Successfully"), true);
                    return RedirectToAction("DisplayCustomerAccounts");
                }
            }
            Danger("Something Went wrong. Please try again", true);
            return View(loanAccountViewModel);
        }
        public ActionResult DisplayLoanAccounts()
        {
            var loanAccountDisplay = _loanAccountLogic.RetrieveAllAccounts();
            return View(loanAccountDisplay);
        }
        [HttpGet]
        public ActionResult EditLoanAccount(string id)
        {
            var loanAccount =_loanAccountLogic.RetrieveLoanAccountById(id);
            LoanAccountViewModel loanAccountViewModel = new LoanAccountViewModel
            {
                EncryptedId = loanAccount.EncryptedId,
                CAccountName = loanAccount.CAccountName,
                BranchId = loanAccount.BranchId,
                Branches = _customerAccountLogic.PopulateBranchDropDown()
            };
            return View(loanAccountViewModel);
     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLoanAccount(LoanAccountViewModel loanAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                bool check = await _loanAccountLogic.EditLoanAccount(loanAccountViewModel);
                if (check)
                {
                    //Display Notification
                    Success(string.Format("{0} Edit Successful", loanAccountViewModel.CAccountName), true);
                    return RedirectToAction(actionName: "DisplayCustomerAccounts");
                }
            }

            //Display Notification
            Danger(string.Format("{0} edit Unsuccessful. Please try again", loanAccountViewModel.CAccountName), true);
            loanAccountViewModel.Branches = _customerAccountLogic.PopulateBranchDropDown();
            return View(loanAccountViewModel);
        }
        public async Task<ActionResult> CloseLoanAccount(string id)
        {
            bool closed = await _loanAccountLogic.CloseAccount(id);
            if (closed)
            {
                //Display Notification Account Closed
                Success("Account Closed Successfully", true);
                return RedirectToAction("DisplayCustomerAccounts");
            }
            //Display Notification
            Danger("Oops! Something Went wrong. Please try again", true);
            return RedirectToAction("DisplayOpenAccounts");
        }
    }
}