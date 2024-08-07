using BibliotecaNexus.Data.Domain.Entidades;

namespace BibliotecaNexus.Models
{
    public class PrestamoVm
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
