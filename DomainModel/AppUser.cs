
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class AppUser : IdentityUser
    {
        // Ajoutez des propriétés supplémentaires si nécessaire
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}