using BibliotecaNexus.Data.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaNexus.Data.Domain
{
    public class LibroConfiguracion : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {
            builder.ToTable("Libros");

            builder.HasKey(e => e.LibroId);
            builder.Property(e => e.Titulo).IsRequired().HasMaxLength(255);
            builder.Property(e => e.ISBN).IsRequired().HasMaxLength(13);
            builder.Property(e => e.Genero).IsRequired().HasMaxLength(50);
            builder.Property(e => e.AnioPublicacion).IsRequired();
            builder.HasOne(e => e.Autor)
                   .WithMany(a => a.Libros)
               .HasForeignKey(e => e.AutorId);
            builder.HasOne(e => e.Categoria)
                   .WithMany(c => c.Libros)
                   .HasForeignKey(e => e.CategoriaId);
            
        }
    }

    public class ActividadesConfiguracion : IEntityTypeConfiguration<Actividades>
    {
        public void Configure(EntityTypeBuilder<Actividades> builder)
        {
            builder.ToTable("Actividades");

            builder.HasKey(e => e.ActividadId);

            builder.Property(e => e.Nombre).HasMaxLength(200).IsRequired();
            builder.Property(e => e.Descripcion).HasMaxLength(500);
            builder.Property(e => e.FechaInicio).IsRequired();
            builder.Property(e => e.FechaFin).IsRequired();
        }
    }

    public class AutorConfiguracion : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autores");

            builder.HasKey(e => e.AutorId);

            builder.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Apellido).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Telefono).HasMaxLength(20);
            builder.Property(e => e.Cargo).HasMaxLength(50);
        }
    }

    public class AutorizacionConfiguracion : IEntityTypeConfiguration<Autorizacion>
    {
        public void Configure(EntityTypeBuilder<Autorizacion> builder)
        {
            builder.ToTable("Autorizaciones");

            builder.HasKey(e => e.AutorizacionId);

            builder.Property(e => e.Modulo).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Permiso).HasMaxLength(100).IsRequired();
        }
    }

    public class CategoriaConfiguracion : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(e => e.CategoriaId);

            builder.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
        }
    }

    public class ClienteConfiguracion : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(e => e.ClienteId);

            builder.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
           
        }
    }

    public class ComentariosConfiguracion : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentarios");

            builder.HasKey(e => e.ComentarioId);

            builder.HasOne(e => e.Cliente)
                   .WithMany()
                   .HasForeignKey(e => e.ClienteId);

            builder.HasOne(e => e.Libro)
                   .WithMany()
                   .HasForeignKey(e => e.LibroId);
        }
    }

    public class PrestamoConfiguracion : IEntityTypeConfiguration<Prestamo>
    {
        public void Configure(EntityTypeBuilder<Prestamo> builder)
        {
            builder.ToTable("Prestamos");

            builder.HasKey(e => e.PrestamoId);

            builder.HasOne(e => e.Cliente)
                   .WithMany()
                   .HasForeignKey(e => e.ClienteId);

            builder.HasOne(e => e.Libro)
                   .WithMany()
                   .HasForeignKey(e => e.LibroId);
        }
    }

    public class ReservaConfiguracion : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");

            builder.HasKey(e => e.ReservaId);

            builder.HasOne(e => e.Cliente)
                   .WithMany()
                   .HasForeignKey(e => e.ClienteId);

            builder.HasOne(e => e.Libro)
                   .WithMany()
                   .HasForeignKey(e => e.LibroId);
        }
    }

    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(e => e.UsuarioId);

            builder.Property(e => e.NombreUsuario).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Contrasena).HasMaxLength(100).IsRequired();
            builder.Property(e => e.TipoUsuario).HasMaxLength(50).IsRequired();
        }
    }
}
