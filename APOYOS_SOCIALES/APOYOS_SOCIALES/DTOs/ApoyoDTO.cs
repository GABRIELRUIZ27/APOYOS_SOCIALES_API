using APOYOS_SOCIALES.Entities;

namespace APOYOS_SOCIALES.DTOs
{
    public class ApoyoDTO 
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string? CURP { get; set; }
        public string Comentarios { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; } 
        public string? Foto { get; set; }
        public string ImagenBase64 { get; set; }
        public ComunidadDTO Comunidad { get; set; }
        public AreaDTO Area { get; set; }
        public GeneroDTO Genero { get; set; }
    }
}