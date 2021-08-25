using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.Identity.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Secondname { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        
    }
}
