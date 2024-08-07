using BibliotecaNexus.Data.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaNexus.Data.Domain
{
    public class BibliotecaNexusDbContext : DbContext
    {
        public BibliotecaNexusDbContext(DbContextOptions<BibliotecaNexusDbContext> options) : base(options) { }

        public DbSet<Libro> Libro { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }
        public DbSet<Autor> Autore { get; set; }
        public DbSet<Autorizacion> Autorizacione { get; set; }
        public DbSet<Actividade> Actividade { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<AgrupadoModulos> AgrupadoModulos { get; set; }
        public DbSet<ModulosRoles> ModulosRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LibroConfiguracion());
            modelBuilder.ApplyConfiguration(new ActividadeConfiguracion());
            modelBuilder.ApplyConfiguration(new AutorConfiguracion());
            modelBuilder.ApplyConfiguration(new AutorizacionConfiguracion());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguracion());
            modelBuilder.ApplyConfiguration(new ClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new ComentarioConfiguracion());
            modelBuilder.ApplyConfiguration(new PrestamoConfiguracion());
            modelBuilder.ApplyConfiguration(new ReservaConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new RolConfiguracion());
            modelBuilder.ApplyConfiguration(new ModuloConfiguracion());
            modelBuilder.ApplyConfiguration(new ModulosRolesConfiguracion());
            modelBuilder.ApplyConfiguration(new AgrupadoModulosConfiguracion());
        }
    }
}

