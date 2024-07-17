using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTOs.Bottle
{
    public class CreateBottleDto
    {
        public string BottleName { get; set; }
        public int BottleYear { get; set; }
        public string WineColor { get; set; }
        public string Appellation { get; set; }
        public int PeakStart { get; set; }
        public int PeakEnd { get; set; }
        public int CellarId { get; set; }
    }
}
