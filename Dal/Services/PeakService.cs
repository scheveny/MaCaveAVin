using DomainModel;
using Dal.IServices;
using System.ComponentModel.DataAnnotations;

namespace Dal.Services
{
    public class PeakService : IPeakService
    {
        public DateTime CalculateIdealPeak(Bottle bottle)
        {
            int idealYear = (bottle.PeakStart + bottle.PeakEnd) / 2;
            bottle.IdealPeak = new DateTime(idealYear, 1, 1);
            return bottle.IdealPeak;
        }
    }
}
