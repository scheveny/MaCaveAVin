using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CellarModel
    {
        public int CellardModelID { get; set; }
        public string CellarBrand { get; set; }
        public int CellarTemperature { get; set; }
        public ICollection<Cellar> Cellars { get; set; }
    }
}
