using System.Linq;
using Backend.Api.Data;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Api.Helpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController: Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration config;

        public LoginController(DataContext context, IConfiguration _config)
        {
            config = _config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Usuarios user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }

            Usuarios usuario = _context.Usuarios.Where( x => x.Username == user.Username).FirstOrDefault();
            if ( usuario == null )
            {
                return NotFound("Not Found");
            }

            if(HashHelper.CheckHash(user.Password, usuario.Password, usuario.Salt))
            {
                var secretKey = config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Username));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                string bearer_token = tokenHandler.WriteToken(createdToken);
                return Ok (new {bearer_token});
                
            }
            else 
            {
                return Forbid();
            }
        }

    }
}