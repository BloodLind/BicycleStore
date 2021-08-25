using BicycleStore.BikesDatabase.Models;
using BicycleStore.BikesDatabase.Repositories;
using BicycleStore.Identity.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers
{
    public class AdminController : Controller
    {
        readonly int countInOnePage = 10;
        private readonly BicycleRepository bicycleRepository;
        private readonly RoleRepository roleRepository;
        private readonly UserRepository userRepository;

        public AdminController(BicycleRepository bicycleRepository,RoleRepository roleRepository, UserRepository userRepository)
        {
            this.bicycleRepository = bicycleRepository;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        public IActionResult Index(int page = 1)
        {
            int bicyclesCount = bicycleRepository.GetAll().Count();
            ViewBag.countPages = bicyclesCount / countInOnePage;
            if ((bicyclesCount % countInOnePage) > 0)
                ViewBag.countPages++;
            return View(bicycleRepository.GetAll().Skip((page - 1) * countInOnePage).Take(countInOnePage));
        }

        public IActionResult CreateOrEdit(string id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                return View(bicycleRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(id)));
            }
        }
        [HttpPost]
        public IActionResult CreateOrEdit(Bicycle bicycle)
        {
            if (ModelState.IsValid)
            {
                bicycleRepository.CreateOrUpdate(bicycle);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var bicycle = bicycleRepository.Get(Guid.Parse(id));
            bicycleRepository.Delete(bicycle);
            
            return RedirectToAction("Index");
        }
    }
}
