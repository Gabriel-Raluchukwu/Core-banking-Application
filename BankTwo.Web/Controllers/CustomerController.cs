using BankTwo.Application.Logic.InterfaceClasses;
using BankTwo.Application.Logic.Utilities;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{
    [Authorize]
    public class CustomerController : AlertController
    {
        ICustomerLogic _customerLogic;
        public CustomerController(ICustomerLogic customerLogic)
        {
            this._customerLogic = customerLogic;
        }

        public ActionResult CreateCustomer()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(CustomerViewModel customerViewModel)
        {
            DateTime currentDateTime = DateTime.Now;
            customerViewModel.DateAdded = currentDateTime;
            customerViewModel.DateLastUpdated = currentDateTime;
            int generatedId = AutoGenerator.GenerateCustomerId();
            var check =_customerLogic.CheckForDuplicates(generatedId);
            while (check)
            {
                generatedId = AutoGenerator.GenerateCustomerId();
                check = _customerLogic.CheckForDuplicates(generatedId);
            }
            customerViewModel.IdentificationNumber = generatedId;
            if (ModelState.IsValid)
            {
                bool isEmailDuplicate = _customerLogic.CheckForDuplicateEmail(customerViewModel.Email);
                if (!isEmailDuplicate)
                {
                    Task<bool> isFinished =Task.Run(() => _customerLogic.AddCustomer(customerViewModel));
                    bool finished = isFinished.Result;
                    if (finished)
                    {
                        Success(string.Format("Customer {0} {1} added Succesfully.",customerViewModel.LastName,customerViewModel.FirstName), true);
                        return RedirectToAction("DisplayCustomers");
                    }
                }
                Danger(string.Format("{0} email already used.",customerViewModel.Email),true);
            }
            Danger("Save Unsuccessful, Please try Again.", true);
            return View(customerViewModel);
        }

        public ActionResult CustomerAccountSelect()
        {
            var customers = _customerLogic.CustomerDisplay();
            return View(customers);
        }

        public ActionResult DisplayCustomers()
        {
            var customers = _customerLogic.CustomerDisplay();
            return View(customers);
        }

        public ActionResult EditCustomer(string id)
        {
            var customer = _customerLogic.RetrieveCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomer(CustomerViewModel customerViewModel)
        {
            customerViewModel.DateLastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                bool check = await _customerLogic.EditCustomer(customerViewModel);
                if (check)
                {
                    Information(String.Format("{0} {1} Edited Succesfully.", customerViewModel.LastName, customerViewModel.FirstName), true);
                    return RedirectToAction(actionName: "DisplayCustomers");
                }
            }
            Danger("Customer Edit Failed.", true);
            return View(customerViewModel);
        }

        public async Task<ActionResult> DeleteCustomer(string id)
        {
            bool check = await _customerLogic.DeleteCustomer(id);
            if (check)
            {
                Information(String.Format("Customer Account Deleted."), true);
                return RedirectToAction(actionName: "DisplayCustomers");
            }
            Danger("Delete Customer Account Failed. Please Try Again.", true);
            return RedirectToAction(actionName: "DisplayCustomers");
        }
    }
}