using APOYOS_SOCIALES.Entities;
using System.Security.Claims;

namespace APOYOS_SOCIALES.Entities
    {
        public class AguaPotable
        {
            public int Id { get; set; }
            public string Domicilio { get; set; }
            public string? Folio { get; set; }
            public string Contrato { get; set; }
            public Comunidad Comunidad { get; set; }
            public string Nombre { get; set; }
            public string? Periodo { get; set; }
            public TipoServicio TipoServicio { get; set; }

    }
}

