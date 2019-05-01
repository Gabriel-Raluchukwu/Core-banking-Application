using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class TransactionAmountCustomValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var transaction = (TransactionViewModel)validationContext.ObjectInstance;

            if (transaction.DebitAmount <= 0)
            {
                return new ValidationResult("Transaction Amount Cannot be less than or equal to Zero");
            }
            return ValidationResult.Success;
        }
    }
}
