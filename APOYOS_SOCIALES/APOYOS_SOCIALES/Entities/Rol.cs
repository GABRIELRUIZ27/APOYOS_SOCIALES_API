using APOYOS_SOCIALES.Entities;
using System.Security.Claims;

namespace APOYOS_SOCIALES.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Claim> Claims { get; set; }
    }
}