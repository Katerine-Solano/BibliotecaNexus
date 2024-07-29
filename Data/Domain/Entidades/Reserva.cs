namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
