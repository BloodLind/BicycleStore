using BicycleStore.Core.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BicycleStore.Core.Infrastructure.Attributes;

namespace BicycleStore.BikesDatabase.Models
{
    public class Bicycle:IGuidKey
    {

       
        
        [Required]
        public string Model { set; get; }
        [Required, UnFiltered]
        public string Tittle { set; get; }
        [Required, UnFiltered]
        public double Price { set; get; }
        [Required]
        public string Color { set; get; }
        [UnFiltered]
        public string Info { set; get; } = "N/A";
        [UnFiltered]
        public virtual ICollection<OrderBicycle> OrderBicycles { set; get; }


        [Key, UnFiltered]
        public Guid Id { get ; set; }
    }
}
