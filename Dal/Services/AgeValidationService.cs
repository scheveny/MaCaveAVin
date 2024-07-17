using Dal.IServices;
using System.ComponentModel.DataAnnotations;

namespace Dal.Services
{
    public class AgeValidationService : IAgeValidationService
    {
        public ValidationResult ValidateAge(DateTime birthday)
        {
            if (birthday > DateTime.Now.AddYears(-18))
            {
                return new ValidationResult("User must be at least 18 years old.");
            }
            return ValidationResult.Success;
        }
    }
}
