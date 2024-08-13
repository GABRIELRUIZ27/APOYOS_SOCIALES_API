using APOYOS_SOCIALES.Entities;

namespace APOYOS_SOCIALES.DTOs
{
    public class SolicitudDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Edad { get; set; }
        public string? CURP { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? Ubicacion { get; set; }
        public ComunidadDTO Comunidad { get; set; }
        public AreaDTO Area { get; set; }
        public GeneroDTO Genero { get; set; }
        public ProgramaSocialDTO ProgramaSocial { get; set; }
        public bool? Estatus { get; set; }
    }
}
