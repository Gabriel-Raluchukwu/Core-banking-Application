using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class AccountConfiguration_Current:Entity
    {

        public double CurrentCreditInterestRate { get; set; }

        public decimal CurrentMinimumBalance { get; set; }

        [ForeignKey("InterestExpenseGL")]
        public int IEAccountId { get; set; }

        public GLAccount InterestExpenseGL { get; set; }

        public double COT { get; set; }

        [ForeignKey("COTIncomeGL")]
        public int COTIncomeId { get; set; }

        public GLAccount COTIncomeGL { get; set; }
    }
}
