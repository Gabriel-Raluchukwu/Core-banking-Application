using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class FinancialReportGLAccount
    {

        public string GLAccountName { get; set; }

        public int GLAccountCode { get; set; }

        public bool IsClosed { get; set; }

        public decimal AccountBalance { get; set; }
    }
}
