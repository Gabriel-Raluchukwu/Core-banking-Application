using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class CustomerAccountViewModel
    {
        public string EncryptedId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required(ErrorMessage ="Please Enter an Account Name"),MaxLength(50)]
        [Display(Name ="Account Name")]
        [RegularExpression(@"^[a-zA-Z ]+$")]
        public string CAccountName { get; set; }

        [Display(Name ="Account Number")]
        public int CAccountNumber { get; set; }

        [Display(Name = "Account Type")]
        public AccountTypeEnum? AccountTypeEnum { get; set; }

        [DataType(DataType.Currency)]
        public decimal AccountBalance { get; set; }

        [Display(Name ="Branch")]
        public IEnumerable<BranchViewModel> Branches { get; set; }

        [Display(Name = "Branch")]
        [Required(ErrorMessage = "Please Select a Branch")]
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string AccountTypeName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }
    }
    public enum AccountTypeEnum
    {
        Savings = 1,
        Current,
    }
}
