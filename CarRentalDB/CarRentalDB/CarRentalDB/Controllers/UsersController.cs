﻿using CarRentalDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        IConfiguration Config;

        public UsersController(IConfiguration c)
        {
            RentalsDb = new CarRentalDbContext();
            Config = c;
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

        // gets a username and password, returns the full user data if a matching user is found
        [HttpGet("registereduser")]
        public IActionResult GetByUsernamePassword([FromBody] User registeredUser )
        {
            IActionResult response = NotFound();

            User userMatch = RentalsDb.Users
                .Where(u => u.UserName == registeredUser.UserName)
                .FirstOrDefault(u => u.Password == registeredUser.Password);

            if (userMatch != null)
            {
                response = Ok(userMatch);
            }

            return response;
        }

        // POST api/<UsersController>
        // TODO: implement id check attribute in user model
        // gets a new user, if the username does not exist in the db, adds the user to db
        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            User existingUsername = RentalsDb.Users.FirstOrDefault(u => u.UserName == newUser.UserName);

            if (existingUsername != null)
            {
                return BadRequest("this username allready exists in our database");
            }

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

        // gets a user and returns token appropriate to "Role"
        [HttpPost("login")]
        public IActionResult GetToken([FromBody] User requestingUser)
        {
            IActionResult response = Unauthorized();
            var existingUser = RentalsDb.Users.FirstOrDefault(user => user.ID == requestingUser.ID);

            // TODO: is this the best way of doing this?
            if (existingUser != null &&
                requestingUser.UserName == existingUser.UserName &&
                requestingUser.Password == existingUser.Password)
            {
                var secretKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Config["jwtConfig:SecretKey"]));

                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, "clientClaims"),
                        new Claim(ClaimTypes.Name, existingUser.UserName),
                        new Claim(ClaimTypes.Role, existingUser.Role),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                var securityToken = new JwtSecurityToken(
                    issuer: Config["jwtConfig:Issuer"],
                    audience: Config["jwtConfig:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                    );

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                response = Ok(new { responseToken = token, requestingUser = existingUser });
            }

            return response;
        }

        // PUT api/<UsersController>/5
        [HttpPut()]
        public IActionResult Put([FromBody] User changedUser)
        {
            IActionResult response = NotFound();

            User existingUser = RentalsDb.Users.FirstOrDefault(u => u.ID == changedUser.ID);

            if (existingUser != null)
            {
                RentalsDb.Entry(existingUser).CurrentValues.SetValues(changedUser);
                RentalsDb.SaveChanges();
                response = Ok(changedUser);
            }

            return response;
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
