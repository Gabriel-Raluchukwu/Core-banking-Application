using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class GLCategory:Entity
    {
        public string EncryptedId { get; set; }

        [Required]
        public string GLCategoryName { get; set; }
     
        public MainCategories MainCategory { get; set; }

        [Required]
        public int MainCategoriesId { get; set; }

        [Required]
        public string GLCategoryDescription { get; set; }

    }
}
