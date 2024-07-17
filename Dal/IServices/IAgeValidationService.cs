using System.ComponentModel.DataAnnotations;

namespace Dal.IServices
{
    public interface IAgeValidationService
    {
        ValidationResult ValidateAge(DateTime birthday);
    }
}
