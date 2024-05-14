using APOYOS_SOCIALES.Entities;
using System.Security.Claims;

namespace APOYOS_SOCIALES.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}