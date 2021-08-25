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
    public class BicycleRepository : AbstractRepository<Bicycle>
    {
        public BicycleRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
