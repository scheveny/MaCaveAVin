using DomainModel;
using MaCaveAVin.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MaCaveAVin.Services
{
    public class PeakService : IPeakService
    {
        public PeakService(Bottle bottle)
        {
            int idealYear = (bottle.PeakStart + bottle.PeakEnd) / 2;
            bottle.IdealPeak = new DateTime(idealYear, 1, 1);
            return bottle.IdealPeak;
        }
    }
}
