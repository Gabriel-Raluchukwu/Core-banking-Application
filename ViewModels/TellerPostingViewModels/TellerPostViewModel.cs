
using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class TellerPostViewModel
    {
        [Required(ErrorMessage ="Please Select an Employee")]
        [Display(Name = "Customer Account")]
        public int CustomerAccountId { get; set; }

        [Required(ErrorMessage = "Please Select a Till Account")]
        [Display(Name = "Till Account")]
        public int GLAccountId { get; set; }

        [Required(ErrorMessage = "Field cannot be Blank")]
        [Display(Name = "Amount to Debit")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        [TellerPostAmountCustomValidation]
        public decimal DebitAmount { get; set; }

        [Required(ErrorMessage = "Field Cannot be Blank ")]
        [Display(Name = "Amount to Credit")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        [Compare("DebitAmount", ErrorMessage = "Debit Amount must be Equal to Credit Amount")]
        public decimal CreditAmount { get; set; }

        [Required(ErrorMessage = "Please Input a Transaction narration")]
        [Display(Name = "Narration")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9 ,.()-_']+$",ErrorMessage = "Special Characters are not allowed")]
        public string Narration { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        [Display(Name ="Teller Posting Type")]
        [Required(ErrorMessage = "Please define posting type")]
        public TellerPostingType? TellerPostingType { get; set; }

        public string GLAccountName { get; set; }

        public string CustomerAccountName { get; set; }

        public int CustomerAccountNumber { get; set; }

        public IEnumerable<GLAccountDropDownViewModel> GLAccounts { get; set; }
    }
}
