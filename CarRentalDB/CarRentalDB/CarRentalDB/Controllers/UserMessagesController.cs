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
            IActionResult response = NotFound();
            UserMessage Message = await RentalsDb.UserMessages.FirstOrDefaultAsync(m => m.ID == id);

            if (Message!=null)
            {
                response = Ok(Message);
            }
            return response;
        }

        // POST api/<UserMessagesController>
        // TODO: make this async?
        [HttpPost]
        public IActionResult Post([FromBody] UserMessage value)
        {
            IActionResult response;

            UserMessage newMessage = value;
            newMessage.ID = Utils.IDGen(RentalsDb.UserMessages);

            RentalsDb.Database.OpenConnection();
            RentalsDb.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.UserMessages ON");
            try
            {
                RentalsDb.UserMessages.Add(newMessage);
                RentalsDb.SaveChanges();
                response = Ok(newMessage);
            }
            catch (Exception e)
            {
                response = BadRequest(e);
            }
            finally
            {
                RentalsDb.Database.CloseConnection();
                RentalsDb.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.UserMessages OFF");
            }
            return response;
        }

        // PUT api/<UserMessagesController>/5
        [HttpPut("{id}")]
        public async Task <IActionResult> Put(int id, [FromBody] UserMessage value)
        {
            IActionResult response;

            try
            {
                await RentalsDb.IDataModelUpdateDb<UserMessage>(value);
                response = Ok(value);
            }
            catch (Exception e)
            {
                response = BadRequest(e);
            }

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
