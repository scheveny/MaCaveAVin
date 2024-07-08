using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Cellar
    {
        public int CellarID { get; set; }
        public required string CellarName { get; set; }
        public int NbRow { get; set; }
        public int NbStackRow { get; set; }
    }
}
