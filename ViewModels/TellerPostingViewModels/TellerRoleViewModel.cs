using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class TellerRoleViewModel
    {
        [Required(ErrorMessage = "Please Select an Employee")]
        [Display(Name = "Customer Account")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please Select a Till Account")]
        [Display(Name = "Till Account")]
        public int GLAccountId { get; set; }

        public IEnumerable<GLAccountDropDownViewModel> GLAccounts { get; set; }
    }
}
