using BicycleStore.Identity.Models;
using BicycleStore.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.Identity.Migrations
{
    public class IdentityUsersMigration
    {
        private readonly UserRepository userRepository;
        private readonly RoleRepository roleRepository;

        public IdentityUsersMigration(UserRepository userRepository, RoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

       public async void Initializate()
        {
            if(userRepository.Users.ToList().Any(x => x.UserRoles.FirstOrDefault(x => x.Role.NormalizedName == "ADMIN") == null))
            {
                User user = new User
                {
                    Email = "root.root@root",
                    Firstname = "root",
                    Secondname = "root"
                };
                await userRepository.AddUserAsync(user, "root");
                Role role = new Role { Name = "Admin" };
                await roleRepository.CreateRoleAsync(role);
                userRepository.AddToRoleAsync(user, role.Name);
            }
        }
    }
}
