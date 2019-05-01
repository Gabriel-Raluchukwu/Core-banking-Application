using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    
    public class Customer:Entity
    {
        public string EncryptedId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string OtherNames { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
