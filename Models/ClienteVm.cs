namespace BibliotecaNexus.Models
{
    public class ClienteVm
    {
        public Guid ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

    }
}