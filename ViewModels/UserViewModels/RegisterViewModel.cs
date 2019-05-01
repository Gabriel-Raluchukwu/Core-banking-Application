using BankTwo.Application.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class RegisterViewModel
    {
        public string EncryptedId { get; set; }

        [Required(ErrorMessage = "Please Input First Name"),MaxLength(40)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Name should contain only Alphabets")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Please Input Last Name"),MaxLength(40)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z-]*$", ErrorMessage = "Name should contain only Alphabets")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Input Middle Name")]
        [Display(Name = "Other Names")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Name should contain only Alphabets")]
        public string OtherNames { get; set; }

        [Required(ErrorMessage ="Please Input User Name")]
        [Display(Name = "User Name")]
        //[Remote("IsUserNameAvailable","Account",ErrorMessage = " UserName Already Taken")]
        [RegularExpression(@"^[a-zA-Z-_0-9]*$", ErrorMessage = "User Name Cannot Contain spaces and special characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please Input an Email Address")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = " Please Input valid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Input a Phone Number"),MinLength(11),MaxLength(13)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "Please enter a valid  Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Branch ID")]
        public int BranchId { get; set; }

        public int? GLAccountId { get; set; }

        public string UserRole { get; set; }

        public Branch Branch { get; set; }

        public IEnumerable<BranchViewModel> BranchList {get; set;}

        public IEnumerable<IdentityRole> IdentityRoles { get; set; }

    }
}
