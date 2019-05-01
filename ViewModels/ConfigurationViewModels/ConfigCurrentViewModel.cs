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
    public class ConfigCurrentViewModel
    {
        [Required(ErrorMessage = "Please Input Interest Rate")]
        [Display(Name = "Interest Rate")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid  Amount")]
        public double CurrentCreditInterestRate { get; set; }

        [Required(ErrorMessage = "Please Input Minimum Balance")]
        [Display(Name = "Minimum Balance")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        public int CurrentMinimumBalance { get; set; }

        [ForeignKey("InterestExpenseGL")]
        [Display(Name = "Interest Expense Account")]
        [Required(ErrorMessage = "Please Select A GL Account")]
        public int IEAccountId { get; set; }

        public GLAccount InterestExpenseGL { get; set; }
    
        [Required(ErrorMessage = "Please input COT charge")]
        [Display(Name = "Commission on Turnover")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        public double COT { get; set; }

        [ForeignKey("COTIncomeGL")]
        [Required(ErrorMessage = "Please Select a GL Account")]
        [Display(Name = "COT Income Account")]
        public int COTIncomeId { get; set; }

        public GLAccount COTIncomeGL { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }

        public IEnumerable<GLAccountDropDownViewModel> GLAccounts { get; set; }
    }
}
