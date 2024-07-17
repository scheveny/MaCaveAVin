using DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Dal.Services
{
    public interface IPeakService
    {
       public DateTime CalculateIdealPeak(Bottle bottle);
    }
}
