using BicycleStore.Core.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.BikesDatabase.Models
{
    public class OrderBicycle 
    {


       
        public Guid BicycleId { set; get; }
    

        
        public Guid OrderId { set; get; }


        public virtual Bicycle  Bicycle{set;get;}
        public virtual Order Order { set; get; }
        public int CountBicycles { set; get; } = 1;



 
    }
}
