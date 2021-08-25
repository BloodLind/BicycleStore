using BicycleStore.Identity.Models;
using BicycleStore.Identity.Repositories;
using BicycleStore.Web.Models.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleRepository roleRepository;
        private readonly UserRepository userRepository;

        public RoleController(RoleRepository roleRepository, UserRepository userRepository)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        public IActionResult Index() => View(roleRepository.Roles.ToList());

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await roleRepository.CreateRoleAsync(new Role() {Name = name });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            Role role = await roleRepository.GetRoleById(id);
            if (role != null)
            {
                var result = await roleRepository.DeleteRoleAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(userRepository.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            User user = await userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                var userRoles = user.UserRoles.Select(x=>x.Role.Name).ToList();
                var allRoles = roleRepository.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            User user = await userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                var userRoles = user.UserRoles.Select(x => x.Role.Name).ToList();
                var allRoles = roleRepository.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await  userRepository.AddToRolesAsync(user, addedRoles);

                await userRepository.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
    
}
