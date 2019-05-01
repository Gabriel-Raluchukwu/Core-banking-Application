using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoanAccountDisplayViewModel
    {
        public string EncryptedId { get; set; }

        public string CAccountName { get; set; }

        public int CAccountNumber { get; set; }

        public string BranchName { get; set; }
    }
}
