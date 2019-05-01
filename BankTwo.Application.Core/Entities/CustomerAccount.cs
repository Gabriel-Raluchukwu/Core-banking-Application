using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class CustomerAccount:Entity
    {
        public string EncryptedId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string CAccountName { get; set; }

        [Required]
        public int CAccountNumber { get; set; }

        [Required]
        public AccountTypeEnum AccountTypeEnum { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        public Branch Branch { get; set; }

        [Required]
        public int BranchId { get; set; }

        [DataType(DataType.Currency)]
        public decimal AccountBalance { get; set; }    

        public decimal DailyInterest { get; set; }

        public int NumberOfWithdrawals { get; set; }

        public decimal Charges { get; set; }

    }
}
