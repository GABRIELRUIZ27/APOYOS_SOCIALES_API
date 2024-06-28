namespace APOYOS_SOCIALES.Entities
{
    public class Incidencia
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; }
        public string Foto { get; set; }
        public Comunidad Comunidad { get; set; }
        public TipoIncidencia TipoIncidencia { get; set; }
    }
}
