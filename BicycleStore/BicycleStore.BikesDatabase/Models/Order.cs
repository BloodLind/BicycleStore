using BicycleStore.Core.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.BikesDatabase.Models
{
    public class Order:IGuidKey
    {
    
        

     
        [EmailAddress]
        [Required]
        public string UserEmail { set; get; }

       public virtual ICollection<OrderBicycle> OrderBicycles { set; get; }
      
        [Key]
        public Guid Id { get; set; }

    }
}
