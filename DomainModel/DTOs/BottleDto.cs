using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class BottleDto
    {
        public int BottleId { get; set; }
        public Cellar Cellar { get; set; }

        public int CellarId { get; set; }
    }
}
