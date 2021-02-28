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
            return RentalsDb.GetByID<Car>(id);
        }

        // POST api/<CarsController>
        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] Car newCar)
        {
            return RentalsDb.Post<Car>("Cars", newCar);
        }

        // PUT api/<CarsController>/5
        [HttpPut]
        //[Authorize(Roles = "Manager")]
        public IActionResult Put([FromBody] Car updatedCar)
        {
            return RentalsDb.Put<Car>(updatedCar);
        }



        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Manager")]
        public IActionResult Delete(int id)
        {
            return RentalsDb.Delete<Car>(id);
        }

        // FIXME: make this a transaction

        [HttpPut("RentOut/{id}")]
        public IActionResult RentOut(int id, [FromBody] RentedCar rentData)
        {
            Car carToRent = RentalsDb.Cars.FirstOrDefault(c => c.ID == id);
            List<IActionResult> actionResults = new List<IActionResult>();

            if (carToRent.AvailableForRent)
            {
                try
                {
                    carToRent.AvailableForRent = false;
                    actionResults.Add(RentalsDb.Put<Car>(carToRent));
                    actionResults.Add(RentalsDb.Post<RentedCar>("RentedCars", rentData));
                }
                catch (Exception e)
                {
                    actionResults.Add(new BadRequestObjectResult(e));
                }
            }
            else
            {
                actionResults.Add(new BadRequestObjectResult($"Car {carToRent.ID} is not available for rent"));
            }

            return Ok(actionResults);
        }

        [HttpPut("RentIn/{id}")]
        public IActionResult RentIn(int id)
        {
            List<IActionResult> actionResults = new List<IActionResult>();
            var returnedCar = RentalsDb.Cars.FirstOrDefault(c => c.ID == id);

            using (var transaction = RentalsDb.Database.BeginTransaction())
            {
                returnedCar.AvailableForRent = true;
                actionResults.Add(RentalsDb.Put<Car>(returnedCar));
                actionResults.Add(RentalsDb.Delete<RentedCar>(id));
            }

            return Ok(actionResults);
        }
    }
}
