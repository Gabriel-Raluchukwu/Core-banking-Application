using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BranchViewModel
    {   
        public int Id { get; set; }

        [Required (ErrorMessage = "Field Cannot be blank")]
        [Display(Name = "Location")]
        [RegularExpression(@"^[A-Za-z0-9]+$",ErrorMessage = " Location cannot contain Special Characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Field Cannot be blank")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[A-Za-z0-9 ,.]+$",ErrorMessage = " Location cannot contain Special Characters")]
        public string Address { get; set; }
       
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateLastUpdated { get; set; }
    }
}
