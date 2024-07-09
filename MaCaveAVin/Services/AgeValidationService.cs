using MaCaveAVin.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MaCaveAVin.Services
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
