using DomainModel;
using System.ComponentModel.DataAnnotations;

namespace MaCaveAVin.Interfaces
{
    public interface IBottleService
    {
        void CalculateIdealPeak(Bottle bottle);
    }
}
