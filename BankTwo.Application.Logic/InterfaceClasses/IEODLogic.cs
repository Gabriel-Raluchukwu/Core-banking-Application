using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface IEODLogic
    {
        //decimal CalculateCOTCharge(decimal AmountWithdrawn);

        bool IsBusinessClosed();

        DateTime RetrieveFinancialDate();

        Task<bool> OpenBusiness();

        Task<bool> CloseBusiness();
       
    }
}
