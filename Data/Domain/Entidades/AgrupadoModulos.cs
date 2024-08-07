namespace BibliotecaNexus.Data.Domain.Entidades
{
    public class AgrupadoModulos
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        public Guid createdBy { get; set; }
        public DateTime FechaRegistro { get; set; }
        public ICollection<Modulo> Modulos { get; set; }
        public AgrupadoModulos() {
            Modulos = new HashSet<Modulo>();
        }

    }
}
