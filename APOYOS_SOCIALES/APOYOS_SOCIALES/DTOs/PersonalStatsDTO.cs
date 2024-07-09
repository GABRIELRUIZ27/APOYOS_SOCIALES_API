namespace APOYOS_SOCIALES.DTOs
{
    public class TotalEmpleadosDTO
    {
        public int TotalEmpleados { get; set; }
    }

    public class EmpleadosPorGeneroDTO
    {
        public Dictionary<string, int> EmpleadosPorGenero { get; set; }
    }

    public class EmpleadosPorAreaDTO
    {
        public Dictionary<string, int> EmpleadosPorArea { get; set; }
    }
    public class TotalSalariosDTO
    {
        public decimal TotalSalarios { get; set; }
    }

    public class TotalAreasDTO
    {
        public int TotalAreas { get; set; }
    }
}
