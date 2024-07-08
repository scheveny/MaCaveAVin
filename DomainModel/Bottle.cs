using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Bottle
    {
        public int BottleID { get; set; }
        public string BottleName { get; set; }
        public int BottleYear { get; set; }
        public string WineColor { get; set; }
        public string Appellation { get; set; }
        public int StorageStartYear { get; set; }
        public int StorageEndYear { get; set; }
        public int DrawerNb { get; set; }
        public int StackInDrawerNb { get; set; }
        public string PeakYears { get; set; }

        public Cellar Cellar { get; set; }

        public int CellarId { get; set; }
    }
}
