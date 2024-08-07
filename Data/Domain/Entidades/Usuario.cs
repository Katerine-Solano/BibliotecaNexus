namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public Guid createdBy { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Eliminado { get; set; }
        public Rol Rol { get; set; }
        public Guid RolId { get; set; }
    }
}
