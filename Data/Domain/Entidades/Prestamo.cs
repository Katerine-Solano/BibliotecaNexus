namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Prestamo
    {
        public Guid PrestamoId { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
