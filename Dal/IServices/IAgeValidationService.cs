using System.ComponentModel.DataAnnotations;

namespace Dal.Services
{
    public interface IAgeValidationService
    {
        ValidationResult ValidateAge(DateTime birthday);
    }
}
