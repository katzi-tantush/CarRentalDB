using CarRentalDB.Models;
using CarRentalDB.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalDB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public LocationsController()
        {
            RentalsDb = new CarRentalDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.Locations);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return RentalsDb.GetResultByID<Location>(id);
        }

        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] Location newLocation)
        {
            return RentalsDb.PostIdGen<Location>("Locations", newLocation);
        }

        [HttpPut]
        //[Authorize(Roles = "Manager")]
        public IActionResult Put(int id, [FromBody] Location modifiedLocation)
        {
            return RentalsDb.Put<Location>(modifiedLocation);
        }

    }
}
