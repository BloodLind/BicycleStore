using BicycleStore.BikesDatabase.Context;
using BicycleStore.BikesDatabase.Models;
using BicycleStore.BikesDatabase.Repositories;
using BicycleStore.Core.Infrastructure.Interfaces;
using BicycleStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        static private readonly int countInOnePage = 10;
        static private  int page = 1;
        static private  string category = null;
        static private string searchText = null;
        static private Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();
        IRepository<Bicycle> bicycleRepository;
        public HomeController( IRepository<Bicycle> bicycleRepository)
        {
           this.bicycleRepository = bicycleRepository;
     
        }

        private List<Bicycle> SortOrFilter(int page, string category=null,string searchText="")
        {
            List<Bicycle> bicycles;
            if (category != null)
            {
                bicycles = bicycleRepository.GetAll().Include(x=>x.Photo).ToList().OrderBy((x => x.GetType().GetProperty(category).GetValue(x))).ToList();
            }
            else
                bicycles = bicycleRepository.GetAll().Include(x => x.Photo).ToList();

            foreach (var pair in HomeController.filters)
                if (pair.Value.Count > 0)
                {
                    foreach (var pare in HomeController.filters)
                        bicycles = bicycles.Where(x => pare.Value.Contains(x.GetType().GetProperty(pare.Key).GetValue(x))).ToList();
                }
            ViewBag.Filters = HomeController.filters;
            if(searchText != null && searchText.Length > 0 )
            {
                bicycles = bicycles.Where(x => x.Tittle.ToLower().Contains(searchText.ToLower())).ToList();
            }
            ViewBag.countPages = (int)Math.Ceiling(bicycles.Count / (double)countInOnePage);
            ViewBag.currentPage = page;
            bicycles = bicycles.Skip((page - 1) * countInOnePage).Take(countInOnePage).ToList();
            return bicycles;
        }
      
      
        [HttpGet("Home/Index/Category-{category}")]
        public IActionResult Index(string category = null)
        {

            HomeController.category = category;
            return View(SortOrFilter(1, category));

        }
        [HttpGet("Home/Index/Page{page:int}") , HttpGet(""), HttpGet("Home/Index")]
        public IActionResult Index(int page = 1)
        {
            HomeController.page = page;
            return View((SortOrFilter(page,searchText:searchText)));
        }
        [HttpGet("Home/Index/Category-{category}/Page{page:int}")]
        public IActionResult Index(int page = 1, string category = null)
        {
            HomeController.category = category;
            HomeController.page = page;
            return View(SortOrFilter(page, category));
        }
        public IActionResult BicycleList(string searchText)
        {
          
            HomeController.searchText = searchText;

            return PartialView("_BicyclesList", SortOrFilter(page,category, searchText));
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

