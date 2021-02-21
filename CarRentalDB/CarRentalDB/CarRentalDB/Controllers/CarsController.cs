using CarRentalDB.Models;
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
        public async Task<IActionResult> Get(int id)
        {
            var car = await RentalsDb.Cars.FirstOrDefaultAsync(c => c.ID == id);

            if (car != null)
            {
                return Ok(car);
            }
            return NotFound();
        }

        // POST api/<CarsController>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        //public IActionResult Post([FromBody] Car value)
        //{
        //    RentalsDb.Database.OpenConnection();
        //    try
        //    {
        //        RentalsDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Cars ON");
        //        RentalsDb.Cars.Add(value);
        //        RentalsDb.SaveChanges();
        //        RentalsDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Cars OFF");

        //        return Ok(value);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //    finally
        //    {
        //        RentalsDb.Database.CloseConnection();
        //    }
        //}

        // FIXME: async version not working - identetiy insert remains set to off
        public async Task<IActionResult> Post([FromBody] Car value)
        {
            await RentalsDb.Database.OpenConnectionAsync();
            try
            {
                await RentalsDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Cars ON");
                await RentalsDb.Cars.AddAsync(value);
                await RentalsDb.SaveChangesAsync();
                await RentalsDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Cars OFF");

                return Ok(value);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            finally
            {
                await RentalsDb.Database.CloseConnectionAsync();
            }
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Car value)
        {
            var existingCar = await RentalsDb.Cars.FirstOrDefaultAsync(c => c.ID == value.ID);

            if (existingCar != null)
            {
                try
                {
                    RentalsDb.Entry(existingCar).CurrentValues.SetValues(value);
                    await RentalsDb.SaveChangesAsync();

                    return Ok(value);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            return NotFound();
        }



        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var carToDelete = await RentalsDb.Cars.FirstOrDefaultAsync(c => c.ID == id);
            if (carToDelete != null)
            {
                RentalsDb.Cars.Remove(carToDelete);
                RentalsDb.SaveChanges();
                return Ok(carToDelete);
            }
            return NotFound();
        }
    }
}
