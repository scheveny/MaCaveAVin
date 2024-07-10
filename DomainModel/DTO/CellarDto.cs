using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class CellarDto
    {
        [Required]
        [StringLength(50)]
        public string CellarName { get; set; }

        [Required]
        public int NbRow { get; set; }

        [Required]
        public int NbStackRow { get; set; }

        [Required]
        public UserDto User { get; set; }

        public int? CellarCategoryId { get; set; }
        public int? CellarModelId { get; set; }
        public ICollection<BottleDto> Bottles { get; set; } = new List<BottleDto>();
    }
}
