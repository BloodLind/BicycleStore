using BicycleStore.BikesDatabase.Models;
using BicycleStore.Core.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers.API
{
    [ApiController, Route("api/[controller]"), Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    public class BicyclesController : Controller
    {
        private readonly IRepository<Bicycle> repository;

        public BicyclesController(IRepository<Bicycle> repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bicycle>> Get(Guid id)
        {

          
            return await Task.Run(() => repository.Get(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicycle>>> Get()
        {
          
            return await repository.GetAll().ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Bicycle>> Post(Bicycle bicycle)
        {
          

            if(bicycle.Price<0)
            {
                ModelState.AddModelError("Price", "your price is huynya!");
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            repository.CreateOrUpdate(bicycle,bicycle.Id);
            repository.SaveChanges();
            return Ok(bicycle);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bicycle>> Delete(string id)
        {

           
            Guid guid = Guid.Parse(id);
            if (guid == Guid.Empty)
                BadRequest();
            repository.Delete(guid);
            repository.SaveChanges();
            return Ok();
        }
       
        [HttpPut]
        public async Task<ActionResult<Bicycle>> Put(Bicycle bicycle)
        {
            if (bicycle == null)
                BadRequest();
            if (repository.Get(bicycle.Id) == null)
                return NotFound();
            else
                return RedirectToAction("Post", bicycle);
        }
    }
            
}
