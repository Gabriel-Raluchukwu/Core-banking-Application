using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class GLCategoryViewModel
    {
        public string EncryptedId { get; set; }

        [Required(ErrorMessage ="Please Input a General Ledger Category Name ")]
        [Display(Name = "GL Category Name")]
        [RegularExpression(@"^[a-zA-Z ]+$")]
        public string GLCategoryName { get; set; }

        public MainCategories MainCategory { get; set; }

        public int MainCategoriesId { get; set; }

        [Required(ErrorMessage = "Please Select a Main Category")]
        [Display(Name = "Main Category")]
        public MainCategoriesEnum? MainCategoryEnum{ get; set; }

        [Required(ErrorMessage = "Please Input Category Description")]
        [Display(Name = "GL Category Description")]
        [RegularExpression(@"^[a-zA-Z0-9 ,.()-_]+$")]
        public string GLCategoryDescription { get; set; }

        [Display(Name = " Date Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }
    }
}
