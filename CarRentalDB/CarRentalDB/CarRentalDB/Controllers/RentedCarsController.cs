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
    public class RentedCarsController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public RentedCarsController()
        {
            RentalsDb = new CarRentalDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.RentedCars);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return RentalsDb.GetResultByID<RentedCar>(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] RentedCar newRentData)
        {
            return RentalsDb.Post<RentedCar>("RentedCars", newRentData);
        }

        [HttpPut()]
        public IActionResult Put([FromBody] RentedCar modifiedRentData)
        {
            return RentalsDb.Put<RentedCar>(modifiedRentData);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return RentalsDb.Delete<RentedCar>(id);
        }
    }
}
