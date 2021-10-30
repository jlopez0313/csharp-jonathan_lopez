using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Api.Models {
    public class Subareas 
    {
        public int Id { get; set; }
        public string Subarea { get; set; }
        public int AreasId { get; set; }
        public Areas Area { get; set; }
        public ICollection<Empleados> Empleados { get; set; }
        
    }
}