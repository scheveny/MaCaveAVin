using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO.cellarModel
{
    public class CreateCellarModelDto
    {
        //public int CellarModelId { get; set; }
        public string CellarBrand { get; set; }
        public int CellarTemperature { get; set; }
        //public string AppUserId { get; set; } // Foreign key to AppUser
        //public AppUser AppUser { get; set; }
        public ICollection<Cellar> Cellars { get; set; }
    }
}
