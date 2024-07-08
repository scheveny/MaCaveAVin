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

        public User User { get; set; }
        public ICollection<Bottle> Bottles { get; set; }

        public int? CellarCategoryID { get; set; }
        public CellarCategory? CellarCategory { get; set; }
        public int? CellarModelId { get; set; }        
        public CellarModel? CellarModel { get; set; }
    }
}
