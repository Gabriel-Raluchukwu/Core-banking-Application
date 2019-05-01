using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class GLAccountDisplayViewModel
    {
        public string EncryptedId { get; set; }

        [Required]
        [Display(Name = "GL Account Name")]
        public string GLAccountName { get; set; }

        [Display(Name = "GL Account Code")]
        public int GLAccountCode { get; set; }

        public int GLCategoryId { get; set; }

        public string GLCategoryName { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = " Date Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime DateLastUpdated { get; set; }

        public bool IsVault { get; set; }

        public string BranchLocation { get; set; }

        public IEnumerable<BranchViewModel> Branches { get; set; }
    }
}
