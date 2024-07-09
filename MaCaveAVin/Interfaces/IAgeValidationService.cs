using System.ComponentModel.DataAnnotations;

namespace MaCaveAVin.Interfaces
{
    public interface IAgeValidationService
    {
        ValidationResult ValidateAge(DateTime birthday);
    }
}
