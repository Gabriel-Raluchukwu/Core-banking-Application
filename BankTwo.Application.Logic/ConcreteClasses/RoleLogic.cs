using BankTwo.Application.Data.InterfaceClasses;
using BankTwo.Application.Logic.InterfaceClasses;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class RoleLogic : IRoleLogic
    {
        public IRoleRepository<IdentityRole> roleRepo;
        public RoleLogic(IRoleRepository<IdentityRole> roleDB)
        {
            roleRepo = roleDB;
        }
        public IEnumerable<IdentityRole> PopulateRolesDropDownList()
        {
            IEnumerable<IdentityRole> RoleList = roleRepo.RetrieveAllFromDB();
                return RoleList;
        }
    }
}
