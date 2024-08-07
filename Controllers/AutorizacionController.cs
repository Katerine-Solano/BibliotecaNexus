using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BibliotecaNexus.Controllers
{
    public class AutorizacionController : Controller
    {
        private readonly ILogger<AutorizacionController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public AutorizacionController(BibliotecaNexusDbContext context, ILogger<AutorizacionController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var autorizaciones = _context.Autorizacione
                .ProjectToType<AutorizacionVm>()
                .ToList();
            return View(autorizaciones);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(AutorizacionVm autorizacion)
        {
            if (ModelState.IsValid)
            {
                var nuevaAutorizacion = new Autorizacion
                {
                    AutorizacionId = Guid.NewGuid(),
                    Modulo = autorizacion.Modulo,
                    Permiso = autorizacion.Permiso
                };

                try
                {
                    _context.Autorizacione.Add(nuevaAutorizacion);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar la nueva autorización.");
                    _logger.LogError(ex, "Error al agregar una nueva autorización");
                }
            }

            return View(autorizacion);
        }

        [HttpGet]
        public IActionResult Editar(Guid autorizacionId)
        {
            var autorizacion = _context.Autorizacione.FirstOrDefault(a => a.AutorizacionId == autorizacionId);
            if (autorizacion == null)
            {
                return NotFound();
            }

            var autorizacionVm = new AutorizacionVm
            {
                AutorizacionId = autorizacion.AutorizacionId,
                Modulo = autorizacion.Modulo,
                Permiso = autorizacion.Permiso
            };

            return View(autorizacionVm);
        }

        [HttpPost]
        public IActionResult Editar(AutorizacionVm autorizacion)
        {
            if (ModelState.IsValid)
            {
                var autorizacionExistente = _context.Autorizacione.FirstOrDefault(a => a.AutorizacionId == autorizacion.AutorizacionId);
                if (autorizacionExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    autorizacionExistente.Modulo = autorizacion.Modulo;
                    autorizacionExistente.Permiso = autorizacion.Permiso;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar la autorización.");
                    _logger.LogError(ex, "Error al editar la autorización");
                }
            }

            return View(autorizacion);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid autorizacionId)
        {
            var autorizacion = _context.Autorizacione.FirstOrDefault(a => a.AutorizacionId == autorizacionId);
            if (autorizacion == null)
            {
                return NotFound();
            }

            try
            {
                _context.Autorizacione.Remove(autorizacion);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar la autorización.");
                _logger.LogError(ex, "Error al eliminar la autorización");
                return View("Index");
            }
        }
    }
}
