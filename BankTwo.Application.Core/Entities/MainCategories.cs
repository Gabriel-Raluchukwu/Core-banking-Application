using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class MainCategories:Entity
    {
        [Required]
        public string MainCategoryName { get; set; }

        [Required]
        public string MainCategoryAccountType { get; set; }

        [Required]
        public int MainCategoryOperation { get; set; }
    }
}
