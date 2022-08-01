using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserPayment.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration config;

        public AuthenticationController(IConfiguration config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public class AuthenticationRequestBody
        {
            public string Username { get; set; } = null;
            public string Password { get; set; } = null;

        }
        private class MiniAdeptUser
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }

            public MiniAdeptUser(int userId, string username, string firstName, string lastName, string city)
            {
                UserId = userId;
                Username = username;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }
        }
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {

            //validate the user/password
            var user = ValidateUserCredentials(authenticationRequestBody.Username, authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            //Create token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Authentication:SecretForKey"]));
            var signCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(config["Authentication:Issuer"], config["Authentication:Audience"], claimsForToken, DateTime.Now, DateTime.Now.AddHours(1), signCredential);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private MiniAdeptUser ValidateUserCredentials(string username, string password)
        {
            return new MiniAdeptUser(1, username, "David", "Scott", "Glasgow");
        }
    }
}
