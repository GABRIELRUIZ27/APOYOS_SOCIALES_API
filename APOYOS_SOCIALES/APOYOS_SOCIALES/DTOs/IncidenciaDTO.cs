using APOYOS_SOCIALES.Entities;

namespace APOYOS_SOCIALES.DTOs
{
    public class IncidenciaDTO
    {
        public int? Id { get; set; }
        public string Comentarios { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; }
        public string? Foto { get; set; }
        public string ImagenBase64 { get; set; }
        public ComunidadDTO Comunidad { get; set; }
        public TipoIncidenciaDTO TipoIncidencia { get; set; }
    }
}
