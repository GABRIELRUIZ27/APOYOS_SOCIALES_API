using System.Collections.Generic;
using System.ComponentModel;

namespace APOYOS_SOCIALES.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } 
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        public Rol Rol { get; set; }
        public int? AreaId { get; set; }
        public Area? Area { get; set; } 

    }
}