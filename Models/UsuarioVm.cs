using BibliotecaNexus.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaNexus.Models
{
    public class UsuarioVm
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }


        public string Email { get; set; }


        public string DNI { get; set; }


        public string Direccion { get; set; }

        public string Telefono { get; set; }


        public string Password { get; set; }

        public RolVm Rol { get; set; }
        public Guid RolId { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<AgrupadoModulosVm> Menu { get; set; }


        public string validacion_administrador()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                return "Porfavor coloque el nombre";
                { }
            }
            return string.Empty;
        }
    }
}