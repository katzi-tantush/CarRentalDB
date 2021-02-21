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
    public class BranchesController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public BranchesController()
        {
            RentalsDb = new CarRentalDbContext();
        }

        // GET: api/<BranchesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.Branches);
        }

        // GET api/<BranchesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult response = NotFound();

            Branch branch = RentalsDb.Branches.FirstOrDefault(b => b.ID == id);

            if (branch != null)
            {
                response = Ok(branch);
            }

            return response;
        }

        // POST api/<BranchesController>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] Branch value)
        {
            try
            {
                RentalsDb.Branches.Add(value);
                RentalsDb.SaveChanges();
                return Ok(value);
            }
            catch (Exception e )
            {
                return BadRequest(e);
            }
        }

        // PUT api/<BranchesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public IActionResult Put(int id, [FromBody] Branch value)
        {
            Branch branch = RentalsDb.Branches.FirstOrDefault(b => b.ID == id);

            if (branch == null)
            {
                return NotFound();
            }

            try
            {
                RentalsDb.Entry(branch).CurrentValues.SetValues(value);
                RentalsDb.SaveChanges();
                return Ok(value);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
