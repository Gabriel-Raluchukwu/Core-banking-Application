using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class AccountConfiguration_Savings:Entity
    {
        [Required]
        public double SavingsCreditInterestRate { get; set; }

        [Required]
        public decimal SavingsMinimumBalance { get; set; }

        [ForeignKey("InterestExpenseGL")]
        public int IEAccountId { get; set; }

        public GLAccount InterestExpenseGL { get; set; }

      //  public int MyProperty { get; set; }
    }
}
