using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [Table("CellarCategory")]
    public class CellarCategory
    {
        public int CellarCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UserId { get; set; } // Foreign key to AppUser
        public AppUser User { get; set; } // Navigation property
        public ICollection<Cellar> Cellars { get; set; }
    }
}
