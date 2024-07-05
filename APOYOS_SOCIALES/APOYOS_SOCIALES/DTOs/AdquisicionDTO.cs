using APOYOS_SOCIALES.Entities;

namespace APOYOS_SOCIALES.DTOs
{
    public class AdquisicionDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string? Folio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public string? Proveedor { get; set; }
        public string? Marca { get; set; }
        public string FechaAdquisicion { get; set; }
        public AreaDTO Area { get; set; }
    }
}
