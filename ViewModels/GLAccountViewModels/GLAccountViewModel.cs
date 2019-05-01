using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class GLAccountViewModel
    {
        public string EncryptedId { get; set; }

        [Required(ErrorMessage = "Please Input General Ledger Account Name")]
        [Display(Name = "GL Account Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Name should contain only Alphabets")]
        public string GLAccountName { get; set; }

        [Display(Name = "GL Account Code")]
        public int GLAccountCode { get; set; }

        [Required(ErrorMessage = "Please Select a category")]
        [Display(Name = "GL Category")]
        public int? GLCategoryId { get; set; }

        public IEnumerable<GLCategoryDropDownViewModel> GLCategories { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        public IEnumerable<BranchViewModel> Branches { get; set; }

        public bool IsClosed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [Display(Name = " Date Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }

    }
}
