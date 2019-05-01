using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class AccountConfiguration_Loan:Entity
    {
        public double DebitInterestRate { get; set; }

        [ForeignKey("InterestIncomeGL")]
        public int InterestIncomeId { get; set; }

        public GLAccount InterestIncomeGL { get; set; }


    }
}
