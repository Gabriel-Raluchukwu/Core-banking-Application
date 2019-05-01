using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public enum Gender {
    Male,
    Female
    }

    public enum MainCategoriesEnum
    {
        Asset =1,
        Liabilities,
        Capital,
        Income,
        Expenses
    }
    public enum AccountTypeEnum
    {
        Savings = 1,
        Current
    }
    public enum GLAccountTypeEnum
    {
        Default,
        Till
    }

    public enum LoanPaymentSchedule
    {
        Monthly = 1,
        Quarterly
    }

    public enum LoanStatus
    {
        LoanOverDue,
        LoanOngoing,
        LoanComplete
    }

    public enum TellerPostingType
    {
        Withdrawal,
        Deposit
    }
}

