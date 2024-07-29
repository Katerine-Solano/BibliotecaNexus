namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Categoria
    {



        public int CategoriaId { get; set; }
        public string Nombre { get; set; }

        public ICollection<Libro> Libros { get; set; }

    }
}
