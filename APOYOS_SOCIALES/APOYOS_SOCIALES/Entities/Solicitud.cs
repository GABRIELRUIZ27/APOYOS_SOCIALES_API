namespace APOYOS_SOCIALES.Entities
{
    public class Solicitud
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string? CURP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; }
        public Comunidad Comunidad { get; set; }
        public Area Area { get; set; }
        public Genero Genero { get; set; }
        public ProgramaSocial ProgramaSocial { get; set; }
        public bool? Estatus { get; set; }
    }
}
