using BibliotecaNexus.Data.Domain.Entidades;

namespace BibliotecaNexus.Models
{
    public class ReservaVm
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
