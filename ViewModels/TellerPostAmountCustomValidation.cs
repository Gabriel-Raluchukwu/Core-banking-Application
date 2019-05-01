using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class TellerPostAmountCustomValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var tellerPost = (TellerPostViewModel)validationContext.ObjectInstance;

            if (tellerPost.DebitAmount <= 0)
            {
                return new ValidationResult("Transaction Amount Cannot be less than or equal to Zero");
            }
            return ValidationResult.Success;
        }
    }
}
