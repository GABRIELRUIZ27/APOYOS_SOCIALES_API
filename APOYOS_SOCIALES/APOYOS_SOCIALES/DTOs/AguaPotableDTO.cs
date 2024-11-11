using APOYOS_SOCIALES.Entities;

namespace APOYOS_SOCIALES.DTOs
{
    public class AguaPotableDTO
    {
        public int? Id { get; set; }
        public string? Domicilio { get; set; }
        public string? Folio { get; set; }
        public string? Contrato { get; set; }
        public ComunidadDTO? Comunidad { get; set; }
        public string? Nombre { get; set; }
        public string? Periodo { get; set; }
        public TipoServicioDTO? TipoServicio { get; set; }
    }
}
