using BicycleStore.BikesDatabase.Models;
using BicycleStore.Core.Infrastructure.Interfaces;
using BicycleStore.Web.Extensions;
using BicycleStore.Web.Models.Cart;
using BicycleStore.Web.Models.ViewModels.Cart;
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

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel() { Cart = GetCart(), ReturnURL = returnUrl });
        }
        public IActionResult AddToCart(string id,string returnURL)
        {
            Cart cart = GetCart();
            Bicycle bicycle = bicycleRepository.Get(Guid.Parse(id));
            cart.AddItem(bicycle);
            SaveCart(cart);
            return Redirect(returnURL);
        }
        public IActionResult RemoveFromCart(string id, string returnURL)
        {
            Cart cart = GetCart();
            Bicycle bicycle = bicycleRepository.Get(Guid.Parse(id));
            cart.Remove(bicycle);
            SaveCart(cart);
            return Redirect(returnURL);
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
