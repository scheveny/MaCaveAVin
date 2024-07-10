using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class UserDto
    {
        [Required]
        public int UserId { get; set; }
        public ICollection<Cellar>? Cellars { get; set; }
    }
}
