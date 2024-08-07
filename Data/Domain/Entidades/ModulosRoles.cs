namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class ModulosRoles
    {
        public Guid Id { get; set; }
        public bool Eliminado { get; set; }
        public Guid createdBy { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Rol Rol { get; set; }
        public Guid RolId { get; set;}
        public Modulo Modulo { get; set; }
        public Guid ModuloId { get; set;}
    }
}
