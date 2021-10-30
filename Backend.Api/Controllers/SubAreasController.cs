using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Api.Data;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubAreasController : ControllerBase
    {
        private readonly DataContext _context;

        public SubAreasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{AreasId}")]
        public IActionResult GetByArea( int AreasId )
        {
            var result = _context.Subareas.Where(x => x.AreasId == AreasId).ToList();
            return Ok( result );
        }

    }
}
