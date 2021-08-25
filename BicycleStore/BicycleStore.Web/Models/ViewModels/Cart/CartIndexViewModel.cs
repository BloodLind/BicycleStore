using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BicycleStore.Web.Models.Cart;

namespace BicycleStore.Web.Models.ViewModels.Cart
{
    public class CartIndexViewModel
    {
        public BicycleStore.Web.Models.Cart.Cart Cart { get; set; }
        public string ReturnURL { get; set; }
    }
}
