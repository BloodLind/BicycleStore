using BicycleStore.BikesDatabase.Models;
using BicycleStore.BikesDatabase.Repositories;
using BicycleStore.Core.Infrastructure.Interfaces;
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
    public class AdminController : Controller
    {
        readonly int countInOnePage = 10;
        private readonly IRepository<Bicycle> bicycleRepository;
        private readonly RoleRepository roleRepository;
        private readonly UserRepository userRepository;

        public AdminController(IRepository<Bicycle> bicycleRepository, RoleRepository roleRepository, UserRepository userRepository)
        {
            this.bicycleRepository = bicycleRepository;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        #region Bicycles
        public IActionResult Bicycles(int page = 1)
        {
            int bicyclesCount = bicycleRepository.GetAll().Count();
            ViewBag.countPages = Math.Ceiling(bicyclesCount / (double)countInOnePage);
            return View(bicycleRepository.GetAll().Skip((page - 1) * countInOnePage).Take(countInOnePage));
        }

        public IActionResult CreateOrEditBicycle(string id)
        {
            if (id == null)
            {
                return View(new Bicycle { Id = Guid.Empty});
            }
            else
            {
                return View(bicycleRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(id)));
            }
        }

       
        public IActionResult SaveBicyclesData(Bicycle bicycle)
        {
            if (ModelState.IsValid)
            {
                bicycleRepository.CreateOrUpdate(bicycle, bicycle.Id);
                bicycleRepository.SaveChanges();
                return RedirectToAction("Bicycles");
            }
            
            return RedirectToAction("CreateOrEditBicycle", "Admin" , (bicycle.Id));
        }


        public IActionResult DeleteBicycle(string id)
        {
            var bicycle = bicycleRepository.Get(Guid.Parse(id));
            bicycleRepository.Delete(bicycle);

            return RedirectToAction("Bicycles");
        }
        #endregion

        #region Users

        public async Task<IActionResult> DeleteUser(string id)
        {
            await userRepository.DeleteUserAsync(await userRepository.GetUserByIdAsync(id));
            return RedirectToAction("EditUsers");
        }

        public async Task<IActionResult> EditUserData(string id)
        {
            return View(await userRepository.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditUserData(User user)
        {
            if (ModelState.IsValid)
            {
                var root = await userRepository.GetUserByIdAsync(user.Id);
                root.UserName = user.Email;
                
                userRepository.UpdateUser(user); 
                return RedirectToAction("UserList", "Role");
            }
            return View(user.Id);
        }
        #endregion
    }
}
