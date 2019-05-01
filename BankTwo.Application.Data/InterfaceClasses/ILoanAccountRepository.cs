using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.InterfaceClasses
{
    public interface ILoanAccountRepository
    {
        Task<bool> AddLoanAccountToDB(LoanAccount loanAccount);
      
        IEnumerable<LoanAccount> RetrieveAllAccounts();

        Task<bool> UpdateLoanAccount(LoanAccount loanAccount);

        LoanAccount RetrieveById(int id);

        Task<bool> UpdateDB(LoanAccount loanAccount);   

    }
}
