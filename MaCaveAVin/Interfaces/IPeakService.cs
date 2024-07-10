using DomainModel;
using System.ComponentModel.DataAnnotations;

namespace MaCaveAVin.Interfaces
{
    public interface IPeakService
    {
        void CalculateIdealPeak(Bottle bottle);
    }
}
