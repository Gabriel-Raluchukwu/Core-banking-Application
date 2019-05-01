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
    public class ConfigSavingsViewModel
    {
        
        [Display(Name ="Interest Rate")]
        [Required(ErrorMessage = "Please Input Interest Rate")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        public double SavingsCreditInterestRate { get; set; }

        [Display(Name = " Minimum Balance")]
        [Required(ErrorMessage = "Please Input Minimum Balance")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        public int SavingsMinimumBalance { get; set; }

        [ForeignKey("InterestExpenseGL")]
        [Required(ErrorMessage ="Please Select a GL Account")]
        [Display(Name = "Interest Income Account")]
        public int IEAccountId { get; set; }

        public GLAccount InterestExpenseGL { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }

        public IEnumerable<GLAccountDropDownViewModel> GLAccounts { get; set; }
    }
}
