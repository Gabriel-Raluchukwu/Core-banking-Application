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
    public class ConfigLoanViewModel
    {
        [Required(ErrorMessage = "Please Input Interest Rate")]
        [Display(Name = "Interest Rate")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        public double DebitInterestRate { get; set; }

        [ForeignKey("InterestIncomeGL")]
        [Required(ErrorMessage ="Please Select a GL Account")]
        [Display(Name ="Interest Income Account")]
        public int InterestIncomeId { get; set; }

        public GLAccount InterestIncomeGL { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }

        public IEnumerable<GLAccountDropDownViewModel> GLAccounts { get; set; }
    }
}
