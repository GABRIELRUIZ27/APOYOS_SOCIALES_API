namespace APOYOS_SOCIALES.DTOs
{
    public class IncidenciasPorDiaDTO
    { 
        public string Fecha { get; set; }
        public int Cantidad { get; set; }
    }

    public class IncidenciaRecurrenteDTO
    {
        public TipoIncidenciaDTO TipoIncidencia { get; set; }
        public int Total { get; set; }
    }

    public class ComunidadRecurrenteDTO
    {
        public ComunidadDTO Comunidad { get; set; }
        public int Total { get; set; }
    }
}
