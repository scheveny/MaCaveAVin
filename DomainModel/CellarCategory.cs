using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CellarCategory
    {
        public int CellarCategoryID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Cellar> Cellars { get; set; }
    }
}
