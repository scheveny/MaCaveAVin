using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainModel
{
    public class AppUser : IdentityUser
    {
        //[Required]
        [StringLength(50)]
        public string? FirstName { get; set; }
        //[Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        //[Required]
        public DateTime? Birthday { get; set; }
        
        [StringLength(80)]
        public string? Address { get; set; }
        
        [StringLength(20)]
        public string? Telephone { get; set; }

        public ICollection<Cellar>? Cellars { get; set; }

    }
}
