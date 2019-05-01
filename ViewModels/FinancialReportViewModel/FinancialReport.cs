using System.Collections.Generic;

namespace ViewModels
{
    public class FinancialReport
    {
        public IEnumerable<FinancialReportGLAccount> AssetAccounts { get; set; }

        public IEnumerable<FinancialReportGLAccount> LiabilitiesAccounts { get; set; }

        public IEnumerable<FinancialReportGLAccount> IncomeAccounts { get; set; }

        public IEnumerable<FinancialReportGLAccount> ExpenseAccounts { get; set; }

        public IEnumerable<FinancialReportGLAccount> CapitalAccounts { get; set; }

        public IEnumerable<FinancialReportCustomerAccount> CustomerAccounts { get; set; }

        public IEnumerable<FinancialReportLoanAccount> LoanAccounts { get; set; }
    }
}
