using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class GLCategoryDisplayViewModel
    {
        public string EncryptedId { get; set; }

        [Required]
        [Display(Name = "GL Category Name")]
        public string GLCategoryName { get; set; }

        public int MainCategoriesId { get; set; }

        [Display(Name = "Main Category")]
        public string MainCategoryName { get; set; }

        [Required]
        [Display(Name = "GL Category Description")]
        public string GLCategoryDescription { get; set; }

        [Display(Name = " Date Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }

    }
}
