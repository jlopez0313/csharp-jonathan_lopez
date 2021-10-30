namespace Backend.Api.Models {
    public class Empleados 
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public double Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int SubareasId { get; set; }
        public Subareas Subarea { get; set; }

    }
}