namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Libro
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Genero { get; set; }
        public int AnioPublicacion { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }

        public Autor Autor { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Prestamo> Prestamos { get; set; }
    }
}
