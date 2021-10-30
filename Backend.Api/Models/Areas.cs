using System.Collections.Generic;

namespace Backend.Api.Models 
{
    public class Areas 
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public ICollection<Subareas> Subareas { get; set; }
    }
}