
using CarRentalDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public LocationsController()
        {
            RentalsDb = new CarRentalDbContext();
        }

        // GET: api/<LocationsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.Locations);
        }

        // GET api/<LocationsController>/5
        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        {
            IActionResult response = NotFound();

            var location = await RentalsDb.Locations.FirstOrDefaultAsync(l => l.ID == id);

            if (location != null)
            {
                response = Ok(location);
            }

            return response;
        }

        // POST api/<LocationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LocationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
