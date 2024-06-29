namespace APOYOS_SOCIALES.Entities 
{
    public class ProgramaSocial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }
        public Area Area { get; set; }
    }
}