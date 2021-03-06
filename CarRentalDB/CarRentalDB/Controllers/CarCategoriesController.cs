﻿using CarRentalDB.Helpers;
using CarRentalDB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalDB.Controllers
{
    // TODO: use authentication for this

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
            var carcategory = RentalDB.CarCategories.FirstOrDefault(category => category.ID == id);
            if (carcategory == null)
            {
                return NotFound();
            }
            return Ok(carcategory);
        }

        // POST api/<CarCategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CarCategory value)
        {
            try
            {
                var category = value;
                value.ID = Utils.IDGen(RentalDB.CarCategories);
                RentalDB.CarCategories.Add(category);
                RentalDB.SaveChanges();
                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<CarCategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarCategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carCategory = RentalDB.CarCategories.FirstOrDefault(category => category.ID == id);
            if (carCategory == null)
            {
                return NotFound();
            }
            RentalDB.CarCategories.Remove(carCategory);
            RentalDB.SaveChanges();
            return Ok(carCategory);
        }
    }
}
