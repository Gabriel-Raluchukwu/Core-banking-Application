using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CustomerViewModel
    {
        public string EncryptedId { get; set; }

        [Required(ErrorMessage = "Please Input your First Name")]
        [Display(Name = "First Name"), MaxLength(40)]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Name should contain only Alphabets")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Input your Last Name")]
        [Display(Name = "Last Name"), MaxLength(40)]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Name should contain only Alphabets")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Input your Middle Name")]
        [Display(Name = "Other Names")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Name should contain only Alphabets")]
        public string OtherNames { get; set; }

        [Required(ErrorMessage = "Please Input an Address")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[A-Za-z0-9 ,.-]+$", ErrorMessage = "Address cannot contain Special Characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Input Phone Number"), MaxLength(13), MinLength(11)]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "Please enter a valid  Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Select a Gender type")]
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }

        [Required]
        public int IdentificationNumber { get; set; }

    }
}
