using APOYOS_SOCIALES.Entities;
using System.Security.Claims;

namespace APOYOS_SOCIALES.Entities
{
    public class PadronAgua
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Importe { get; set; }
        public string Descripcion { get; set; }
        public string Periodo { get; set; }
        public string Servicio { get; set; }
        public string Alcantarillado { get; set; }
        public AguaPotable Agua { get; set; }
        public bool? Inapam { get; set; }
        public bool Pago { get; set; }
        public string? Folio { get; set; }

    }
}
