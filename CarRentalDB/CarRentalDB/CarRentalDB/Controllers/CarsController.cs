using CarRentalDB.Helpers;
using CarRentalDB.Models;
using CarRentalDB.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.Cars);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return RentalsDb.GetResultByID<Car>(id);
        }

        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] Car newCar)
        {
            return RentalsDb.Post<Car>("Cars", newCar);
        }

        [HttpPut]
        //[Authorize(Roles = "Manager")]
        public IActionResult Put([FromBody] Car updatedCar)
        {
            return RentalsDb.Put<Car>(updatedCar);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Manager")]
        public IActionResult Delete(int id)
        {
            return RentalsDb.Delete<Car>(id);
        }

    }
}
