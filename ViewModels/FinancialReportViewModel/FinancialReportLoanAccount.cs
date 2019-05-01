using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class FinancialReportLoanAccount
    {
        public int CustomerAccountNumber { get; set; }
       
        public decimal LoanAccountBalance { get; set; }

        public decimal LoanPrincipal { get; set; }

        public decimal MonthlyLoanBalance { get; set; }
    }
}
