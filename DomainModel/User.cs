using System.Text.Json.Serialization;

namespace DomainModel
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }

        public ICollection<Cellar>? Cellars { get; set; }

        

    }
}
