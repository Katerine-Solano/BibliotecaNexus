using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BibliotecaNexus.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BibliotecaNexusDbContext _context;

        public UsuarioController(BibliotecaNexusDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UsuarioVm vm)
        {
            var usuario = _context.Usuario.Where(u => u.Eliminado == false & u.Email == vm.Email).ProjectToType<UsuarioVm>().FirstOrDefault();
            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no existen";
                return View(new UsuarioVm());
            }
            if (usuario.Password != Utilidades.Utilidades.GetMD5(vm.Password))
            {
                ViewBag.Error = "Usuario o contraseña no existen";
                return View(new UsuarioVm());
            }
            var modulosroles = _context.ModulosRoles.Where(u => u.Eliminado == false && u.RolId == usuario.Rol.Id).ProjectToType<ModulosRolesVm>().ToList();
            var agrupadosId = modulosroles.Select(ms => ms.Modulo.AgrupadoModulosId).Distinct().ToList();
            var agrupados = _context.AgrupadoModulos.Where(u => agrupadosId.Contains(u.Id)).ProjectToType<AgrupadoModulosVm>().ToList();
            foreach (var item in agrupados)
            {
                var modulosactuales = modulosroles.Where(u => u.Modulo.AgrupadoModulosId == item.Id).Select(ms => ms.Modulo.Id).Distinct().ToList();
                item.Modulos = item.Modulos.Where(u => modulosactuales.Contains(u.Id)).ToList();
            }
            usuario.Menu = agrupados;
            var sesionjson = JsonConvert.SerializeObject(usuario);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(sesionjson);
            var sesionbas64 = System.Convert.ToBase64String(plainTextBytes);
            HttpContext.Session.SetString("UsuarioObjeto", sesionbas64);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CambiarContraseña()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContraseña(string nuevaContraseña, string confirmarContraseña)
        {
            if (string.IsNullOrEmpty(nuevaContraseña) || string.IsNullOrEmpty(confirmarContraseña))
            {
                TempData["mensaje"] = "Por favor ingrese una nueva contraseña y confirme la contraseña.";
                return RedirectToAction("CambiarContraseña");
            }

            if (nuevaContraseña != confirmarContraseña)
            {
                TempData["mensaje"] = "Las contraseñas no coinciden.";
                return RedirectToAction("CambiarContraseña");
            }

            // Obtener la información del usuario de la sesión
            var sesionBase64 = HttpContext.Session.GetString("UsuarioObjeto");
            var base64EncodedBytes = System.Convert.FromBase64String(sesionBase64);
            var usuario = JsonConvert.DeserializeObject<UsuarioVm>(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));

            if (usuario == null)
            {
                // Manejar el caso en que el usuario no esté en la sesión
                TempData["mensaje"] = "Usuario no encontrado";
                return RedirectToAction("Index", "Home"); // O redirecciona a donde desees
            }

            var usuarioNuevo = _context.Usuario.FirstOrDefault(u => u.Id == usuario.Id);

            if (usuarioNuevo == null)
            {
                TempData["mensaje"] = "Usuario no encontrado";
                return RedirectToAction("Index", "Home");
            }

            usuarioNuevo.Password = Utilidades.Utilidades.GetMD5(nuevaContraseña);

            _context.SaveChanges();
            TempData["mensaje"] = "Cambio de Contraseña Exitoso";


            HttpContext.Session.Remove("UsuarioObjeto");

            return RedirectToAction("Index");
        }

    }
}
