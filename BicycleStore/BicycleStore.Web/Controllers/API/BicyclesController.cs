using BicycleStore.BikesDatabase.Models;
using BicycleStore.Core.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers.API
{
    [ApiController, Route("api/[controller]")]
    public class BicyclesController : Controller
    {
        private readonly IRepository<Bicycle> repository;

        public BicyclesController(IRepository<Bicycle> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicycle>>> Get()
        {
            return await repository.GetAll().ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Bicycle>> Post(Bicycle bicycle)
        {
            if (bicycle == null)
                BadRequest();
            repository.CreateOrUpdate(bicycle);
            return Ok(bicycle);
        }
        [HttpDelete]
        public async Task<ActionResult<Bicycle>> Delete(Bicycle bicycle)
        {
            
            if (bicycle == null || bicycle.Id == Guid.Empty)
                BadRequest();
            repository.Delete(bicycle);
            return Ok(bicycle);
        }
        [HttpPut]
        public async Task<ActionResult<Bicycle>> Put(Bicycle bicycle)
        {
            if (bicycle == null)
                BadRequest();
            if (repository.Get(bicycle.Id) == null)
                return NotFound();
            else
                return RedirectToAction("Get", bicycle);
        }
    }
            
}
