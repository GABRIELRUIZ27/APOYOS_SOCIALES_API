namespace APOYOS_SOCIALES.Entities
{
    public class Apoyo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string? CURP { get; set; } 
        public string Comentarios { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; }
        public string Foto { get; set; }
        public Comunidad Comunidad { get; set; }
        public Area Area { get; set; }
        public Genero Genero { get; set; }
    }
}