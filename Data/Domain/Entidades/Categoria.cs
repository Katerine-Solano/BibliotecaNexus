namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Categoria
    {



        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; }

        public ICollection<Libro> Libros { get; set; }

    }
}
