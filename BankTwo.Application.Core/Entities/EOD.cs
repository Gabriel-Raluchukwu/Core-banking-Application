using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class EOD
    {
        public int Id { get; set; }

        public bool IsClosed { get; set; }

        public DateTime FinancialDate { get; set; }
       
    }
}
