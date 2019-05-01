using BankTwo.Application.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class GLAccountDropDownViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string GLAccountName { get; set; }

        [Required]
        public int GLAccountCode { get; set; }

        public GLCategory GLCategory { get; set; }
    }
}
