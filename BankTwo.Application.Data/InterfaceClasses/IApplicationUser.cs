using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.InterfaceClasses
{
    public interface IApplicationUser
    {
        Task<bool> InsertToDB(ApplicationUser user);
        ApplicationUser RetrieveById(string id);
        IEnumerable<ApplicationUser> RetrieveAllFromDB();
        Task<bool> UpdateDB(ApplicationUser user);
        Task<bool> DeleteFromDB(string id);

        string RetrieveRoleName(string id);
        List<String> RetrieveUserNames();
    }
}
