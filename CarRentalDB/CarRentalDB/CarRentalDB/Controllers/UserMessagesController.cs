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


        // GET: api/<UserMessagesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.UserMessages);
        }

        // GET api/<UserMessagesController>/5
        [HttpGet("{id}")]
        public async Task  <IActionResult> Get(int id)
        {
            return RentalsDb.GetByID<UserMessage>(id);
        }

        // POST api/<UserMessagesController>
        // TODO: make this async?
        [HttpPost]
        public async Task <IActionResult> Post([FromBody] UserMessage value)
        {
            IActionResult response = await RentalsDb.PostIdGen<UserMessage>("UserMessages", value);

            return response;
        }

        // PUT api/<UserMessagesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserMessage value)
        {
            IActionResult response = await RentalsDb.Put<UserMessage>(value);
            
            return response;
        }

        // DELETE api/<UserMessagesController>/5
        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            IActionResult response = await RentalsDb.Delete<UserMessage>(id);
            return response;
        }
    }
}
