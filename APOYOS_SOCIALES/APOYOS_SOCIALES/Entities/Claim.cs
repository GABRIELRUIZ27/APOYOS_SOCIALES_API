using APOYOS_SOCIALES.Entities;
using System.ComponentModel.DataAnnotations;

namespace APOYOS_SOCIALES.Entities 
{
    public class Claim
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public bool ClaimValue { get; set; }
        [Required]
        public Rol Rol { get; set; }
    }
}