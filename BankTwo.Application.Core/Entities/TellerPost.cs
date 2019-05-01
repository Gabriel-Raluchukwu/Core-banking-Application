using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class TellerPost
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("CustomerAccount")]
        public int CustomerAccountId { get; set; }

        [Required]
        [ForeignKey("TillGLAccount")]
        public int GLAccountId { get; set; }
        
        public GLAccount TillGLAccount { get; set; }

        public CustomerAccount CustomerAccount { get; set; }

        [Required]
        public decimal DebitAmount { get; set; }

        [Required]
        public decimal CreditAmount { get; set; }

        [Required]
        public string Narration { get; set; }

        [DataType(DataType.DateTime)]
        //All Transaction Daytes are Financial dates
        public DateTime TransactionDate { get; set; }

        [Required]
        public TellerPostingType TellerPostingType { get; set; }
    }
}
