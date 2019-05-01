using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class TransactionViewModel
    {
        [Required(ErrorMessage = "Please Select a GL Account")]
        [Display(Name = "Debit Account")]
        public int DebitGLAccountId { get; set; }

        [Required(ErrorMessage = "Please Select a GL Account")]
        [Display(Name ="Credit Account")]
        public int CreditGLAccountId { get; set; }

        [Required(ErrorMessage = "Field cannot be Blank")]
        [Display(Name = "Amount to Debit")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        [TransactionAmountCustomValidation]
        public decimal DebitAmount { get; set; }

        [Required(ErrorMessage = "Field Cannot be Blank ")]
        [Display(Name = "Amount to Credit")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        [Compare("DebitAmount",ErrorMessage = "Debit Amount must be Equal to Credit Amount")]
        public decimal CreditAmount { get; set; }

        [Required(ErrorMessage = "Please Input a Transaction narration")]
        [Display(Name = "Narration")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9 ,.()-_']+$", ErrorMessage = "Special Characters are not allowed")]
        public string Narration { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        public string DebitGLAccountName { get; set; }

        public string CreditGLAccountName { get; set; }

        public int DebitGLAccountNumber { get; set; }

        public int CreditGLAccountNumber { get; set; }

        public IEnumerable<GLAccountDropDownViewModel> GLAccountDropDown { get; set; }
    }
}
