using BicycleStore.Identity.Models;
using BicycleStore.Identity.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleRepository roleRepository;
        private readonly UserRepository userRepository;

        public RoleController(RoleRepository roleRepository, UserRepository userRepository)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        public IActionResult UserRoles()
        {
            ViewBag.Roles = roleRepository.Roles.ToList();
            return View(userRepository.Users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> UserRoles(List<User> users)

        {
            //foreach (var user in users)
            //{
            //    if (user.UserRoles.First().RoleId == "none")
            //        continue;

            //    var oldUser = await userRepository.GetUserByIdAsync(user.Id);

            //    if (oldUser.UserRoles == null)
            //    var role = user.UserRoles.FirstOrDefault(x => x.UserId == user.Id).Role;
            //    {
            //        await userRepository.AddToRoleAsync(oldUser, role.Name);
            //        continue;
            //    }

            //    var oldRole = oldUser.UserRoles.First().Role;
            //    if (oldRole.Id != user.UserRoles.First().RoleId)
            //    {
            //        await userRepository.RemoveFromRoleAsync(oldUser, oldRole.Name);

            //        await userRepository.AddToRoleAsync(oldUser, roleManager.Roles.FirstOrDefault(x => x.Id == user.UserRoles.First().RoleId).Name);

            //    }
            //}
            
            return RedirectToAction("EditUsers");
        }
    }
}
