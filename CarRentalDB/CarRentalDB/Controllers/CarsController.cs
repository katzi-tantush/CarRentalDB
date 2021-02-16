using CarRentalDB.Models;
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
    public class CarsController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public CarsController()
        {
            RentalsDb = new CarRentalDbContext();
        }
        // GET: api/<CarsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.Cars);
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var car = RentalsDb.Cars.FirstOrDefault(c => c.ID == id);

            if (car != null)
            {
                return Ok(car);
            }
            return NotFound();
        }

        // POST api/<CarsController>
        [HttpPost]
        public IActionResult Post([FromBody] Car value)
        {
            try
            {
                RentalsDb.Cars.Add(value);
                RentalsDb.SaveChanges();

                return Ok(value);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carToDelete = RentalsDb.Cars.FirstOrDefault(c => c.ID == id);
            if (carToDelete != null)
            {
                return Ok(carToDelete);
            }
            return NotFound();
        }
    }
}
