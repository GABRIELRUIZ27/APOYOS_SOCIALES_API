using APOYOS_SOCIALES.Entities;

namespace APOYOS_SOCIALES.DTOs
{
    public class FondoDTO
    {
        public int? Id { get; set; }
        public decimal? Cantidad { get; set; }
        public string? Periodo { get; set; }
        public TipoDistribucionDTO TipoDistribucion { get; set; }
    }
}
