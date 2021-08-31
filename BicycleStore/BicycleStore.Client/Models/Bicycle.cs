
using System;

namespace BicycleStore.Client.Models
{
    public class Bicycle
    {

       
        
     
        public string Model { set; get; }
       
        public string Tittle { set; get; }

        public double Price { set; get; }
    
        public string Color { set; get; }

        public string Info { set; get; }
        public Guid Id { get ; set; }
    }
}
