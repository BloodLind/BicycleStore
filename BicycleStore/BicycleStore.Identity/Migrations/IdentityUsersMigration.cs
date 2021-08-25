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
         
            if (userRepository.Users.ToList().Any(x => x.UserRoles.FirstOrDefault(x => x.Role.NormalizedName == "ADMIN") == null)
                || userRepository.Users.Count() == 0)
            {
                User user = new User
                {
                    UserName = "user@user.user",
                    Email = "user@user.user",
                    Firstname = "user",
                    Secondname = "user"
                };
                await userRepository.AddUserAsync(user, "user");
                User root = new User
                {
                    UserName = "root.root@root",
                    Email = "root.root@root",
                    Firstname = "root",
                    Secondname = "root"
                };
                var res = await userRepository.AddUserAsync(root, "root");
                Role adminRole = new Role { Name = "Admin" };
                Role userRole = new Role { Name = "User" };
                Role moderRole = new Role { Name = "Moderator" };
                await roleRepository.CreateRoleAsync(moderRole);
                await roleRepository.CreateRoleAsync(adminRole);
                await roleRepository.CreateRoleAsync(userRole);
                await userRepository.AddToRoleAsync(root, adminRole.Name);
            }
        }
    }
}
