using CarRentalDB.Helpers;
using CarRentalDB.Models;
using CarRentalDB.Utilities;
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
    public class UserMessagesController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public UserMessagesController()
        {
            RentalsDb = new CarRentalDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.UserMessages);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return RentalsDb.GetResultByID<UserMessage>(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserMessage newUserMessage)
        {
            return RentalsDb.PostIdGen<UserMessage>("UserMessages", newUserMessage);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UserMessage modifiedUserMessage)
        {
            return RentalsDb.Put<UserMessage>(modifiedUserMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return RentalsDb.Delete<UserMessage>(id);
        }
    }
}
