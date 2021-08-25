using BicycleStore.BikesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Models.Cart
{
    public class Cart
    {
        private List<CartLine>  CartLines { set; get; }

        public Cart()
        {
            CartLines = new List<CartLine>();
        }


        public void AddItem(Bicycle bicycle,int quantity)
        {
            CartLine cartLine = CartLines.FirstOrDefault(x => x.Bicycle.Id == bicycle.Id);
            if (cartLine == null)
            {
                CartLines.Add(new CartLine() { Bicycle = bicycle, Quantity = quantity });
            }
            else
                cartLine.Quantity += quantity;
        }
        public void SetItemQuantity(Bicycle bicycle,int quantity)
        {
            CartLine cartLine = CartLines.FirstOrDefault(x => x.Bicycle.Id == bicycle.Id);

            cartLine.Quantity = quantity;
            
           
        }
        public void Remove (Bicycle bicycle)
        {
            CartLines.RemoveAll(x => x.Bicycle.Id == bicycle.Id);

        }

        public void Clear()
        {
            CartLines.Clear();
        }
        public double ComputeTotalValue()
        {
           return CartLines.Sum(x => x.Bicycle.Price * x.Quantity);
        }
        public  IEnumerable<CartLine> Lines => CartLines;

    }

    public class CartLine

    {

        public Bicycle Bicycle {set;get;}
        public int Quantity { set; get; }
    }

}
