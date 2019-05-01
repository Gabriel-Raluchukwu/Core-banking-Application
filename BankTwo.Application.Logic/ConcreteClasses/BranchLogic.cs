using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;
using System.Threading.Tasks;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class BranchLogic:IBranchLogic
    {
        public IBranch<Branch> branchDb;
        public BranchLogic(IBranch<Branch> branch )
        {
            branchDb = branch;
        }

        public async Task<bool> AddNewBranch(BranchViewModel branchViewModel)
        {
           var branch = Mapper.Map<BranchViewModel, Branch>(branchViewModel);
           return await branchDb.InsertToDB(branch);
        }

        public IEnumerable<BranchViewModel> PopulateBranchDropDownList()
        {
            IEnumerable <BranchViewModel> BranchList =
                branchDb.RetrieveAllFromDB().Select(Mapper.Map<Branch,BranchViewModel>);
                return BranchList;

        }
        public IEnumerable<BranchViewModel> BranchDisplay()
        {
            IEnumerable<BranchViewModel> branchViewModels;
            var branches = branchDb.RetrieveAllFromDB();
            if (branches != null)
            {
                branchViewModels = branches.Select(Mapper.Map<Branch,BranchViewModel>).ToList();
                return branchViewModels;
            }
            return null;
        }
    }
}
