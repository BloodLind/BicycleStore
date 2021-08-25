using BicycleStore.BikesDatabase.Models;
using BicycleStore.Core.Infrastructure.Interfaces;
using BicycleStore.Web.Extensions;
using BicycleStore.Web.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Bicycle> bicycleRepository;
        public CartController(IRepository<Order> orderRepo,IRepository<Bicycle> bicycleRepo)
        {

            orderRepository = orderRepo;
            bicycleRepository = bicycleRepo;

        }
        public Task<IActionResult>  AddToCart(string id,string rreturnURL)
        {
            Cart cart = GetCart();
            Bicycle bicycle = bicycleRepository.Get(id);
            return View();
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObjectAsJson("bicycleCart", cart);
        }
        private Cart GetCart( )
        {
       return  HttpContext.Session.GetObjectFromJson<Cart>("bicycleCart")?? new Cart();
        }
    }
}
