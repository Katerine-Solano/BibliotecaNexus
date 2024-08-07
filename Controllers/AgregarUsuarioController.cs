using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaNexus.Controllers
{
    public class AgregarUsuarioController : Controller
    {
        private readonly ILogger<AgregarUsuarioController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public AgregarUsuarioController(ILogger<AgregarUsuarioController> logger, BibliotecaNexusDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _context.Usuario.Where(r => r.Eliminado == false).ProjectToType<UsuarioVm>().ToList();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            var roles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsroles = roles.ConvertAll(d => {
                return new SelectListItem
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            UsuarioVm usuario = new UsuarioVm();
            usuario.Roles = itemsroles;
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Insertar(UsuarioVm usuario)
        {
            if (!string.IsNullOrEmpty(usuario.validacion_administrador()))
            {
                TempData["mensaje"] = usuario.validacion_administrador();
                return RedirectToAction("Registro");
            }


            Usuario nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                DNI = usuario.DNI,
                Direccion = usuario.Direccion,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Password = Utilidades.Utilidades.GetMD5(usuario.Password),
                RolId = usuario.RolId,
                createdBy = Guid.Empty,
                FechaRegistro = DateTime.Now,
                Eliminado = false
            };

            _context.Usuario.Add(nuevoUsuario);
            _context.SaveChanges();

            TempData["mensaje"] = "Usuario registrado correctamente.";
            return RedirectToAction("Index");
        }
        // Acción para mostrar el formulario de edición de usuario
        [HttpGet]
        public IActionResult Editar(Guid UsuarioId)
        {
            var roles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsroles = roles.ConvertAll(d => {
                return new SelectListItem
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            var usuario = _context.Usuario.Where(w => w.Id == UsuarioId).ProjectToType<UsuarioVm>().FirstOrDefault();
            usuario.Roles = itemsroles;
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioVm usuario)
        {
            if (!string.IsNullOrEmpty(usuario.validacion_administrador()))
            {
                TempData["mensaje"] = usuario.validacion_administrador();
                return View(usuario);
            }

            var usuarioNuevo = _context.Usuario.Where(w => w.Id == usuario.Id).FirstOrDefault();


            usuarioNuevo.Nombre = usuario.Nombre;
            usuarioNuevo.Apellido = usuario.Apellido;
            usuarioNuevo.DNI = usuario.DNI;
            usuarioNuevo.Direccion = usuario.Direccion;
            usuarioNuevo.Telefono = usuario.Telefono;
            usuarioNuevo.Email = usuario.Email;
            usuarioNuevo.Password = Utilidades.Utilidades.GetMD5(usuario.Password);
            usuarioNuevo.RolId = usuario.RolId;


            _context.SaveChanges();

            TempData["mensaje"] = "Usuario actualizado correctamente.";
            return RedirectToAction("Index");
        }
        // Acción para mostrar el formulario de eliminación de usuario
        [HttpGet]
        public IActionResult Eliminar(Guid usuarioId)
        {
            var usuario = _context.Usuario.Where(u => u.Id == usuarioId && !u.Eliminado).ProjectToType<UsuarioVm>().FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            var roles = _context.Rol.Where(r => !r.Eliminado).Select(r => new SelectListItem
            {
                Text = r.Descripcion,
                Value = r.Id.ToString(),
                Selected = r.Id == usuario.RolId
            }).ToList();
            usuario.Roles = roles;

            return View(usuario);
        }

        // Acción para eliminar un usuario
        [HttpPost]
        public IActionResult Eliminar(UsuarioVm usuario)
        {
            var usuarioEliminar = _context.Usuario.FirstOrDefault(u => u.Id == usuario.Id && !u.Eliminado);
            if (usuarioEliminar == null)
            {
                return NotFound();
            }

            usuarioEliminar.Eliminado = true;
            _context.SaveChanges();

            TempData["mensaje"] = "Usuario eliminado correctamente.";
            return RedirectToAction("Index");
        }
    }
}