using BibliotecaNexus.Data.Domain.Entidades;

namespace BibliotecaNexus.Models
{
    public class ComentarioVm
    {
        public Guid ComentarioId { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
