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
    public class VaultViewModel
    {
        [Required(ErrorMessage = " Please Select a GL Account")]
        [ForeignKey("GLAccount")]
        [Display(Name = "GL Account")]
        public int GLAccountId { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please input Vault amount")]
        [RegularExpression(@"^[0-9.+]+$", ErrorMessage = "Please enter a valid Amount")]
        public decimal VaultAmount { get; set; }


        public IEnumerable<GLAccountDropDownViewModel> GLAccountDropDown { get; set; }
    }
}
