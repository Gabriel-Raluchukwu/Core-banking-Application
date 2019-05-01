using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class Branch:Entity
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
