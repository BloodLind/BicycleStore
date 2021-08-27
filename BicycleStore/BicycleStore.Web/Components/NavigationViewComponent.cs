using BicycleStore.BikesDatabase.Models;
using BicycleStore.BikesDatabase.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Components
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(typeof(Bicycle).GetProperties().Where(x => x.Name != "Id" && x.Name != "OrderBicycles").Select(x => x.Name));
        }
    }
}
