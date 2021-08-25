using BicycleStore.BikesDatabase.Models;
using BicycleStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.BikesDatabase.Repositories
{
    public class OrderRepository : AbstractRepository<Order>
    {
        public OrderRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
