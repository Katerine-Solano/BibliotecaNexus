namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Actividades
    {
        public int ActividadId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
