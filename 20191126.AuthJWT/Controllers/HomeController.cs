using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using _20191126.AuthJWT.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace _20191126.AuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly string secretOptions;

        public HomeController(IOptions<SecretOptions> secretOptions)
        {
            this.secretOptions = secretOptions.Value.JWTSecret;
        }

        [HttpGet]
        public IActionResult GetSecureInfo()
        {
            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlZhc3lhIiwibmJmIjoxNTc1NjUwOTM1LCJleHAiOjE3OTY1NzU3MzUsImlhdCI6MTU3NTY1MDkzNX0.ocbT7t7PEKEhVTU1t98jNf3FOihPPYTfFKZz7yUJBis";
            //string secret = "bb32e44a-b893-42d1-bea9-7ceb865a8aeb";
            var key = Encoding.ASCII.GetBytes(secretOptions);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
                
            };
            var token =  HttpContext.Request.Headers["Authorization"];
            token = token.ToString().Replace("Bearer ", "");

            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            return Ok(new { name = claims.Identity.Name });
        }
    }
}