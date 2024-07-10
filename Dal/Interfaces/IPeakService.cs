using DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Dal.Interfaces
{
    public interface IPeakService
    {
       public DateTime CalculateIdealPeak(Bottle bottle);
    }
}
