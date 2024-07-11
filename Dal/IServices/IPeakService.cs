using DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Dal.IServices
{
    public interface IPeakService
    {
       public DateTime CalculateIdealPeak(Bottle bottle);
    }
}
