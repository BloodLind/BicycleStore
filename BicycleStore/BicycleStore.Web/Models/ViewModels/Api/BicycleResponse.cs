using BicycleStore.BikesDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Models.ViewModels.Api
{
    public class BicycleResponse
    {
        [Required]
        public string Model { set; get; }
        [Required]
        public string Tittle { set; get; }
        [Required]
        public double Price { set; get; }
        [Required]
        public string Color { set; get; }
       
        public string Info { set; get; } = "N/A";
      
        public Guid Id { get; set; }
     
        public string Image { get; set; }
        

    }
}
