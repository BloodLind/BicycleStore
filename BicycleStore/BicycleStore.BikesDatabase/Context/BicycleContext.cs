using BicycleStore.BikesDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.BikesDatabase.Context
{
    public class BicycleContext : DbContext
    {

        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderBicycle> OrderBicycle { set; get; }
        public DbSet<Photo> Photos { set; get; }



        public BicycleContext( DbContextOptions<BicycleContext> options) : base(options)
        {

            
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderBicycle>().HasKey(x => new { x.OrderId, x.BicycleId });
            modelBuilder.Entity<Bicycle>().HasOne(x => x.Photo);
            
            

        }

    }
}
