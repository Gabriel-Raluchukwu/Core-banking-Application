
namespace ViewModels
{
    public class FinancialReportCustomerAccount
    {

        public int CAccountNumber { get; set; }

        public AccountTypeEnum AccountTypeEnum { get; set; }

        public bool IsClosed { get; set; }

        public virtual decimal AccountBalance { get; set; }
     
    }
}
