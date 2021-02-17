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
    // TODO: only users and admins can delete users and get user data
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public UsersController()
        {
            RentalsDb = new CarRentalDbContext();
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.Users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = RentalsDb.Users.FirstOrDefault(u => u.ID == id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        // POST api/<UsersController>
        // TODO: implement id check attribute in user model
        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            RentalsDb.Database.OpenConnection();
            try
            {
                RentalsDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
                RentalsDb.Users.Add(newUser);
                RentalsDb.SaveChanges();
                RentalsDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            finally
            {
                RentalsDb.Database.CloseConnection();
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = RentalsDb.Users.FirstOrDefault(u => u.ID == id);

            if (user != null)
            {
                RentalsDb.Users.Remove(user);
                RentalsDb.SaveChanges();

                return Ok(user);
            }

            return NotFound();
        }
    }
}
