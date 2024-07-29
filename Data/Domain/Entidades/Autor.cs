namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Cargo { get; set; }

        public ICollection<Libro> Libros { get; set; }
    }
}
