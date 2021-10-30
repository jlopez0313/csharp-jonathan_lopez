using System.Text.Json.Serialization;

namespace Backend.Api.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Salt { get; set; }
    }
}