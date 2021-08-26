using BicycleStore.BikesDatabase.Models;
using BicycleStore.Core.Infrastructure.Attributes;
using BicycleStore.Core.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BicycleStore.Web.Components
{
    public class FiltersViewComponent : ViewComponent
    {
        private readonly IRepository<Bicycle> repository;

        public FiltersViewComponent(IRepository<Bicycle> repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            
            ViewBag.Filters = RouteData.Values["filters"]?.ToString().Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            ViewBag.Filters = ViewBag.Filters == null ? new List<string>() : ViewBag.Filters;
            Dictionary<string, List<string>> filter = new Dictionary<string, List<string>>();
            
            foreach (var key in typeof(Bicycle).GetProperties())
            {
                
                    object[] attrs = key.GetCustomAttributes(true);
                bool isFiltred = true;
                    foreach (object attr in attrs)
                    {
                
                        UnFilteredAttribute authAttr = attr as UnFilteredAttribute;
                        if (authAttr != null || key.PropertyType != typeof(string))
                            {
                                isFiltred = false;
                                break;
                            }
                        
                    }
                if (isFiltred)
                {
                    var list = repository.GetAll().ToList();
                    filter.Add(key.Name, list.
                    Select(x => x.GetType().GetProperty(key.Name).GetValue(x)?.ToString()).Where(x => x != null).Distinct().ToList());
                }
            }
            return View(filter);
        }
    }
}
