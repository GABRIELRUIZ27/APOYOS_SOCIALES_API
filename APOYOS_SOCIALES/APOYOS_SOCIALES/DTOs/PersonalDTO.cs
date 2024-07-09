using APOYOS_SOCIALES.Entities;

namespace APOYOS_SOCIALES.DTOs
{
    public class PersonalDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Edad { get; set; }
        public CargoDTO Cargo { get; set; } 
        public string? FechaContratacion { get; set; }
        public decimal? Salario { get; set; }
        public GeneroDTO Genero { get; set; }
        public AreaDTO? Area { get; set; }
    }
}
