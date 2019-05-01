using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class Transaction
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("DebitGLAccount")]
        public int DebitGLAccountId { get; set; }

        [Required]
        [ForeignKey("CreditGLAccount")]
        public int CreditGLAccountId { get; set; }

        public GLAccount DebitGLAccount { get; set; }

        public GLAccount CreditGLAccount { get; set; }
        [Required]
        public decimal DebitAmount { get; set; }

        [Required]
        public decimal CreditAmount { get; set; }

        [Required]
        public string Narration { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

    }
}
