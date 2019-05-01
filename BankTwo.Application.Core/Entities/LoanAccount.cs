using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class LoanAccount
    {
        public int Id { get; set; }

        public string EncryptedId { get; set; }

        [Required]
        public string CAccountName { get; set; }

        [Required]
        public int CAccountNumber { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        public Branch Branch { get; set; }

        public int CustomerAccountId { get; set; }
        [Required]
        public decimal LoanAccountBalance { get; set; }

        [Required]
        public decimal LoanPrincipal { get; set; }

        [Required]
        public int LoanDuration { get; set; }

        [Required]
        public decimal  MonthlyLoanBalance { get; set; }

        [Required]
        public DateTime LoanStartDate { get; set; }

        [Required]
        public DateTime LoanDueDate { get; set; }

        [Required]
        public LoanPaymentSchedule LoanPaymentSchedule { get; set; }

        [Required]
        public LoanStatus LoanStats { get; set; }
    }
}
