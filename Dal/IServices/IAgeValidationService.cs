using System.ComponentModel.DataAnnotations;

namespace Dal.Interfaces
{
    public interface IAgeValidationService
    {
        ValidationResult ValidateAge(DateTime birthday);
    }
}
