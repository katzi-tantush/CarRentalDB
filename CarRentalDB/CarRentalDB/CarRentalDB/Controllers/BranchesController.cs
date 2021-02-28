using CarRentalDB.FullModels;
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
    [Route("[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        CarRentalDbContext RentalsDb;

        public BranchesController()
        {
            RentalsDb = new CarRentalDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RentalsDb.Branches);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return RentalsDb.GetResultByID<Branch>(id);
        }

        [HttpGet("Full/{id}")]
        public IActionResult GetFull(int id)
        {
            IActionResult response = NotFound();
            FullBranch branch = new FullBranch(RentalsDb, id);
            if (branch != null)
            {
                response = Ok(branch);
            }
            return response;
        }

        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] Branch newBranch)
        {
            return RentalsDb.PostIdGen<Branch>("Branches", newBranch);
        }

        [HttpPut]
        //[Authorize(Roles = "Manager")]
        public IActionResult Put(int id, [FromBody] Branch modifiedBranch)
        {
            return RentalsDb.Put<Branch>(modifiedBranch);
        }
    }
}
