using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Core.Entities
{
    public class GLAccount:Entity
    {
        public string EncryptedId { get; set; }

        [Required]
        public string GLAccountName { get; set; }

        [Required]
        public int GLAccountCode { get; set; }

        public GLCategory GLCategory { get; set; }

        [Required]
        public int GLCategoryId { get; set; }

        public int User { get; set; }

        [Required]

        public GLAccountTypeEnum GLAccountType { get; set; } = GLAccountTypeEnum.Default;

        public Branch Branch { get; set; }

        [Required]
        public int BranchId { get; set; }

        public bool IsClosed { get; set; }

        public bool IsVault { get; set; }

        public decimal AccountBalance { get; set; }

    }
}
