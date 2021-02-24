using CarRentalDB.Models;
using CarRentalDB.Utilities;
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
        [HttpGet]
        public IActionResult Get(int id)
        {
            return RentalsDb.GetByID<Branch>(id);
        }

        // POST api/<BranchesController>
        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] Branch newBranch)
        {
            return RentalsDb.PostIdGen<Branch>("Branches", newBranch);
        }

        // PUT api/<BranchesController>/5
        [HttpPut]
        //[Authorize(Roles = "Manager")]
        public IActionResult Put(int id, [FromBody] Branch modifiedBranch)
        {
            return RentalsDb.Put<Branch>(modifiedBranch);
        }
    }
}
