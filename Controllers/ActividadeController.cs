using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 

namespace BibliotecaNexus.Controllers
{
    public class ActividadeController : Controller
    {
        private readonly ILogger<ActividadeController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public ActividadeController(BibliotecaNexusDbContext context, ILogger<ActividadeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var actividades = _context.Actividade.ProjectToType<ActividadesVm>().ToList();
            return View(actividades);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(ActividadesVm actividad)
        {
            if (ModelState.IsValid)
            {
                var nuevaActividad = new Actividade
                {
                    ActividadId = Guid.NewGuid(),
                    Nombre = actividad.Nombre,
                    Descripcion = actividad.Descripcion,
                    FechaInicio = actividad.FechaInicio,
                    FechaFin = actividad.FechaFin
                };

                try
                {
                    _context.Actividade.Add(nuevaActividad);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar la nueva actividad.");
                    _logger.LogError(ex, "Error al agregar una nueva actividad");
                }
            }

            return View(actividad);
        }

        [HttpGet]
        public IActionResult Editar(Guid actividadId)
        {
            var actividad = _context.Actividade.FirstOrDefault(a => a.ActividadId == actividadId);
            if (actividad == null)
            {
                return NotFound();
            }

            var actividadVm = new ActividadesVm
            {
                ActividadId = actividad.ActividadId,
                Nombre = actividad.Nombre,
                Descripcion = actividad.Descripcion,
                FechaInicio = actividad.FechaInicio,
                FechaFin = actividad.FechaFin
            };

            return View(actividadVm);
        }

        [HttpPost]
        public IActionResult Editar(ActividadesVm actividad)
        {
            if (ModelState.IsValid)
            {
                var actividadExistente = _context.Actividade.FirstOrDefault(a => a.ActividadId == actividad.ActividadId);
                if (actividadExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    actividadExistente.Nombre = actividad.Nombre;
                    actividadExistente.Descripcion = actividad.Descripcion;
                    actividadExistente.FechaInicio = actividad.FechaInicio;
                    actividadExistente.FechaFin = actividad.FechaFin;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar la actividad.");
                    _logger.LogError(ex, "Error al editar la actividad");
                }
            }

            return View(actividad);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid actividadId)
        {
            var actividad = _context.Actividade.FirstOrDefault(a => a.ActividadId == actividadId);
            if (actividad == null)
            {
                return NotFound();
            }

            try
            {
                _context.Actividade.Remove(actividad);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar la actividad.");
                _logger.LogError(ex, "Error al eliminar la actividad");
                return View("Index");
            }
        }
    }
}
