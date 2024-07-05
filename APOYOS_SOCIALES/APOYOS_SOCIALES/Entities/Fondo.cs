namespace APOYOS_SOCIALES.Entities
{
    public class Fondo
    {
        public int Id { get; set; }
        public decimal Cantidad { get; set; }
        public string Periodo { get; set; }
        public TipoDistribucion TipoDistribucion { get; set; }
    }
}
