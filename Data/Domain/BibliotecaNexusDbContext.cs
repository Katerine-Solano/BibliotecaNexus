using BibliotecaNexus.Data.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaNexus.Data.Domain
{
    public class BibliotecaNexusDbContext : DbContext
    {
        public BibliotecaNexusDbContext(DbContextOptions<BibliotecaNexusDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Autorizacion> Autorizaciones { get; set; }
        public DbSet<Actividades> Actividades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LibroConfiguracion());
            modelBuilder.ApplyConfiguration(new ActividadesConfiguracion());
            modelBuilder.ApplyConfiguration(new AutorConfiguracion());
            modelBuilder.ApplyConfiguration(new AutorizacionConfiguracion());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguracion());
            modelBuilder.ApplyConfiguration(new ClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new ComentariosConfiguracion());
            modelBuilder.ApplyConfiguration(new PrestamoConfiguracion());
            modelBuilder.ApplyConfiguration(new ReservaConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
        }
    }
}

