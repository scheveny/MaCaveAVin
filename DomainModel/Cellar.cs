using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [Table("Cellar")]
    public class Cellar
    {
        public int CellarId { get; set; }
        [Required]
        [StringLength(50)]
        public required string CellarName { get; set; }

        [Required]
        public int NbRow { get; set; }

        [Required]
        public int NbStackRow { get; set; }

        public AppUser User { get; set; }
        public ICollection<Bottle> Bottles { get; set; }

        public int? CellarCategoryId { get; set; }
        public CellarCategory? CellarCategory { get; set; }
        public int? CellarModelId { get; set; }        
        public CellarModel? CellarModel { get; set; }
    }
}
