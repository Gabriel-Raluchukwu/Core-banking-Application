using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoanAccountViewModel
    {
        public string EncryptedId { get; set; }

        [Required(ErrorMessage = "Please Input Account Name")]
        [RegularExpression(@"^[a-zA-Z -+]+$", ErrorMessage = "Account Name Cannot Contrain numbers and special characters")]
        [Display(Name = "Account Name")]
        public string CAccountName { get; set; }

        public int CAccountNumber { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int CustomerAccountId { get; set; }

        public int CustomerAccountNumber { get; set; }

        [Required(ErrorMessage = "Please Input Loan Principal")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "Please enter a valid Number")]
        [Display(Name ="Principal")]
        public decimal LoanPrincipal { get; set; }

        [Required(ErrorMessage = "Please Input Loan Duration ")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "Please enter a valid Number")]
        [Display(Name ="Duration of Loan in Months" )]
        public int LoanDuration { get; set; }

        public bool IsClosed { get; set; }

        public decimal LoanAccountBalance { get; set; }

        public decimal MonthlyLoanBalance { get; set; }

        [Required(ErrorMessage = " Field Cannot be blank")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "Please enter a Number")]
        [Range(1, 31, ErrorMessage = "Please Enter a Valid Day Between 1 and 31")]
        public int Day { get; set; }

        [Required(ErrorMessage = " Field Cannot be blank")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "Please enter a Number")]
        [Range(1, 12, ErrorMessage = "Please Enter a Valid Month Between 1 and 12")]
        public int Month { get; set; }

        [Required(ErrorMessage = " Field Cannot be blank")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "Please enter a Number")]
        [Range(2019, 9999, ErrorMessage = "Please Enter a Valid Year")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime LoanStartDate { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime LoanDueDate { get; set; }

        //[Required(ErrorMessage = "Please Select a Payment Schedule")]
        [Display(Name = "Payment Schedule")]
        public LoanPaymentSchedule? LoanPaymentSchedule { get; set; }

        [Required]
        [Display(Name = "Loan Status")]
        public LoanStatus LoanStats { get; set; }

        [Display(Name = "Branch")]
        public IEnumerable<BranchViewModel> Branches { get; set; }

        public enum LoanStatus
        {
            LoanOverDue,
            LoanOngoing,
            LoanComplete
        }
    }
}
