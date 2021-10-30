using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
    public class EmpleadosController : ControllerBase
    {
        private readonly DataContext _context;

        public EmpleadosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int start, [FromQuery] int limit, [FromQuery] string filtro)
        {
            var queryCount = _context.Empleados
                        .OrderBy(x => x.Documento);

            if ( filtro != null ) {
                queryCount = (IOrderedQueryable<Empleados>)queryCount
                        .Where( 
                            x => 
                            x.Nombres.ToLower().Contains( filtro.ToLower() ) ||
                            x.Documento.ToString().Contains( filtro.ToLower() )
                        );
            }

            var queryList = queryCount
                        .Include( x => x.Subarea.Area )
                        .Skip(start)
                        .Take(limit)
                        .ToList();
            
            var total = Math.Ceiling( (double) queryCount.Count() / limit);
            var metadata = new
            {
                total,
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(metadata));

            
            return Ok( queryList );
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _context.Empleados
                        .Include(s => s.Subarea.Area)
                        .FirstOrDefault(x => x.Id == id);
            return Ok( result );
        }
    
        [HttpPost]
        public ActionResult Post([FromBody] Empleados empleado)
        {
            var empl = _context.Empleados.FirstOrDefault(x => x.Documento == empleado.Documento);
            if (empl == null ) {
                _context.Empleados.Add(empleado);
                _context.SaveChanges();
                return Ok();
            } else {
                return Conflict("Error, el documento ya existe!");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Empleados empleado)
        {
            var empl = _context.Empleados.FirstOrDefault(x => x.Documento == empleado.Documento && x.Id != empleado.Id);
            
            if (empl == null ) {
                var _empleado = _context.Empleados.Find( id );
                _empleado.TipoDocumento = empleado.TipoDocumento;
                _empleado.Documento     = empleado.Documento;
                _empleado.Nombres       = empleado.Nombres;
                _empleado.Apellidos     = empleado.Apellidos;
                _empleado.SubareasId    = empleado.SubareasId;

                _context.Empleados.Update(_empleado);
                _context.SaveChanges();
                return Ok(_empleado);
            } else {
                return Conflict("Error, el documento ya existe!");
            }
        }
    }
}
