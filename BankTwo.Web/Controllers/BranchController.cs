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
    public class BranchController : AlertController
    {
        IBranchLogic _branchLogic;
        public BranchController(IBranchLogic branchLogic)
        {
            _branchLogic = branchLogic;
        }

        public ActionResult AddBranch()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBranch(BranchViewModel branchViewModel)
        {
            DateTime CurrentDateTime = DateTime.Now;
            branchViewModel.DateAdded = CurrentDateTime;
            branchViewModel.DateLastUpdated = CurrentDateTime;
            if (ModelState.IsValid)
            {
               var result = await _branchLogic.AddNewBranch(branchViewModel);
                if (result)
                {
                    Success("Branch Added Successfully",true);
                    return RedirectToAction("DisplayBranches");
                }
                Danger("Error Occured, Branch not added",true);
                return View(branchViewModel);
            }
            return View(branchViewModel);
        }
        public ActionResult DisplayBranches()
        {
            var branches = _branchLogic.BranchDisplay();
            return View(branches);
        }
    }
}