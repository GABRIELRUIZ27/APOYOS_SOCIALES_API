namespace APOYOS_SOCIALES.Entities
{
    public class Personal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; } 
        public string FechaContratacion { get; set; } 
        public decimal Salario { get; set; }
        public Genero Genero { get; set; }
        public Cargo Cargo { get; set; }
        public Area? Area { get; set; } 

    }
}
