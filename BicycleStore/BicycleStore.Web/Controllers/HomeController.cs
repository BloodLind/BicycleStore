using BicycleStore.BikesDatabase.Context;
using BicycleStore.BikesDatabase.Models;
using BicycleStore.BikesDatabase.Repositories;
using BicycleStore.Core.Infrastructure.Interfaces;
using BicycleStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers
{
    public class HomeController : Controller
    {

        static private Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();
        IRepository<Bicycle> bicycleRepository;
        public HomeController( IRepository<Bicycle> bicycleRepository)
        {
           this.bicycleRepository = bicycleRepository;
        }

        public IActionResult Index(string category, int?page)
        {
            List<Bicycle> bicycles;
            if (category != null)
            {
                bicycles = bicycleRepository.GetAll().ToList().OrderBy((x => x.GetType().GetProperty(category).GetValue(x))).ToList();
            }
            else
                bicycles = bicycleRepository.GetAll().ToList();

            foreach(var pair in HomeController.filters)
                if(pair.Value.Count >0)
            {
                foreach (var pare in HomeController.filters)
                    bicycles = bicycles.Where(x => pare.Value.Contains(x.GetType().GetProperty(pare.Key).GetValue(x))).ToList();
            }
            ViewBag.Filters = HomeController.filters;
            return View(bicycles);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpPost]
        public IActionResult GenerateFilters(Dictionary<string, List<string>> data)
        {

            filters = data;
            return RedirectToAction("Index");
        }
    }
}

