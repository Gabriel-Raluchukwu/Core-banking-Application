using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface IRoleLogic
    {
        IEnumerable<IdentityRole> PopulateRolesDropDownList();
    }
}
