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
    public class RentedCarsController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public RentedCarsController()
        {
            RentalsDb = new CarRentalDbContext();
        }


        // GET: api/<RentalCarsController>
        [HttpGet]
        [Authorize(Roles = "Employee, Manager")]
        public IActionResult Get()
        {
            return Ok(RentalsDb.RentedCars);
        }

        // GET api/<RentalCarsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee, Manager")]
        public IActionResult Get(int id)
        {
            IActionResult response = NotFound();

            RentedCar rentedCar = RentalsDb.RentedCars.FirstOrDefault(c => c.CarID == id);

            if (rentedCar != null)
            {
                response = Ok(rentedCar);
            }

            return response;
        }

        // POST api/<RentalCarsController>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] RentedCar newRentedCar)
        {
            try
            {
                RentalsDb.RentedCars.Add(newRentedCar);
                RentalsDb.SaveChanges();
                return Ok(newRentedCar);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<RentalCarsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public IActionResult Put([FromBody] RentedCar updatedRentedCar)
        {
            IActionResult response = NotFound();

            var existingCar = RentalsDb.RentedCars.FirstOrDefault(c => c.CarID == updatedRentedCar.CarID);

            if (existingCar != null)
            {
                RentalsDb.Entry(existingCar).CurrentValues.SetValues(updatedRentedCar);
                RentalsDb.SaveChanges();
                response = Ok(existingCar);
            }

            return response;
        }

        // DELETE api/<RentalCarsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee, Manager")]
        public IActionResult Delete(int id)
        {
            IActionResult response = NotFound();

            var carToRemove = RentalsDb.RentedCars.FirstOrDefault(c => c.CarID == id);

            if (carToRemove != null)
            {
                RentalsDb.RentedCars.Remove(carToRemove);
                RentalsDb.SaveChanges();
                response = Ok(carToRemove);
            }

            return response;
        }
    }
}
