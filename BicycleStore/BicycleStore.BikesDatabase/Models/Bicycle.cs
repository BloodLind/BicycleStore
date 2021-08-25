using BicycleStore.Core.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.BikesDatabase.Models
{
    public class Bicycle:IGuidKey
    {

       
        
        [Required]
        public string Model { set; get; }
        [Required]
        public string Tittle { set; get; }
        [Required]
        public double Price { set; get; }
        [Required]
        public string Color { set; get; }
        [Required]
        public string Info { set; get; }
        public virtual ICollection<OrderBicycle> OrderBicycles { set; get; }


        [Key]
        public Guid Id { get ; set; }
    }
}
