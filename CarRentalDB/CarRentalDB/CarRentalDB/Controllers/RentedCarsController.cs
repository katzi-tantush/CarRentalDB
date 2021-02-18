using CarRentalDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    [Authorize(Roles = "Manager")]
    public class RentedCarsController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public RentedCarsController()
        {
            RentalsDb = new CarRentalDbContext();
        }


        // GET: api/<RentalCarsController>
        [HttpGet]
        [Authorize(Roles = "Employee")]
        [Authorize(Roles = "Manager")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RentalCarsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]
        [Authorize(Roles = "Manager")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RentalCarsController>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RentalCarsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RentalCarsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public void Delete(int id)
        {
        }
    }
}
