using CarRentalDB.Models;
using Microsoft.AspNetCore.Mvc;
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
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IConfiguration config;
        CarRentalDbContext RentalDb;

        public LoginController(IConfiguration Config)
        {
            config = Config;
            RentalDb = new CarRentalDbContext();
        }

        //POST api/<LoginController>
        // TODO: change the authentication of the incoming user: db should check username and password
        [HttpPost]
        public IActionResult Post([FromBody] User incomingUser)
        {
            IActionResult response = Unauthorized();
            var existingUser = RentalDb.Users.FirstOrDefault(user => user.ID == incomingUser.ID);

            // TODO: is this the best way of doing this?
            if (existingUser != null && 
                incomingUser.UserName == existingUser.UserName && 
                incomingUser.Password == existingUser.Password)
            {
                var secretKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["jwtConfig:SecretKey"]));

                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new Claim[]
                    {
                        // FIXME: whats this 'abc'?
                        new Claim(JwtRegisteredClaimNames.Sub, "clientClaims"),
                        new Claim(ClaimTypes.Name, existingUser.UserName),
                        new Claim(ClaimTypes.Role, existingUser.Role),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                var securityToken = new JwtSecurityToken(
                    issuer: config["jwtConfig:Issuer"],
                    audience: config["jwtConfig:Audience"],
                    claims: claims, 
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                    );

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                response = Ok(new { responseToken = token, requestingUser = existingUser });
            }

                return response;
        }





        // GET: api/<LoginController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<LoginController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        // PUT api/<LoginController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<LoginController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
