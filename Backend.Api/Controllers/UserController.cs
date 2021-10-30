using Backend.Api.Data;
using Backend.Api.Helpers;
using Backend.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        DataContext _context ;
        public UserController(DataContext ctx)
        {
            _context = ctx;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register()
        {
            Usuarios user = new Usuarios();

            HashedPassword password1 = HashHelper.Hash("123456");
            user.FirstName = "Jonathan";
            user.LastName = "Lopez";
            user.Username = "jlopez";
            user.Password = password1.Password;
            user.Salt = password1.Salt;

            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return Ok();
        }

    }
}