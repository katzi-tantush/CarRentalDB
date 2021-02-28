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
    // FIXME: authorization not working

    [Route("[controller]")]
    [ApiController]
    public class CarCategoriesController : ControllerBase
    {
        CarRentalDbContext RentalDB = new CarRentalDbContext();
        // GET: api/<CarCategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalDB.CarCategories);
        }

        // GET api/<CarCategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return RentalDB.GetResultByID<CarCategory>(id);
        }

        // POST api/<CarCategoriesController>
        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] CarCategory newCarCategory)
        {
            return RentalDB.Post<CarCategory>("CarCategories", newCarCategory);
        }

        // PUT api/<CarCategoriesController>/5
        [HttpPut]
        //[Authorize(Roles = "Manager")]
        public IActionResult Put([FromBody] CarCategory modifiedCarCategory)
        {
            return RentalDB.Put<CarCategory>(modifiedCarCategory);
        }

        // DELETE api/<CarCategoriesController>/5
        //[HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int id)
        {
            return RentalDB.Delete<CarCategory>(id);
        }
    }
}
