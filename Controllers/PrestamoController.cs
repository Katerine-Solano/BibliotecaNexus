using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace BibliotecaNexus.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly ILogger<PrestamoController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public PrestamoController(BibliotecaNexusDbContext context, ILogger<PrestamoController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var prestamos = _context.Prestamo
                .Include(p => p.Cliente)
                .Include(p => p.Libro)
                .ProjectToType<PrestamoVm>()
                .ToList();
            return View(prestamos);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(PrestamoVm prestamo)
        {
            if (ModelState.IsValid)
            {
                var nuevoPrestamo = new Prestamo
                {
                    PrestamoId = Guid.NewGuid(),
                    ClienteId = prestamo.ClienteId,
                    LibroId = prestamo.LibroId
                };

                try
                {
                    _context.Prestamo.Add(nuevoPrestamo);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar el nuevo préstamo.");
                    _logger.LogError(ex, "Error al agregar un nuevo préstamo");
                }
            }

            return View(prestamo);
        }

        [HttpGet]
        public IActionResult Editar(Guid prestamoId)
        {
            var prestamo = _context.Prestamo
                .Include(p => p.Cliente)
                .Include(p => p.Libro)
                .FirstOrDefault(p => p.PrestamoId == prestamoId);
            if (prestamo == null)
            {
                return NotFound();
            }

            var prestamoVm = new PrestamoVm
            {
                Id = prestamo.PrestamoId,
                ClienteId = prestamo.ClienteId,
                Cliente = prestamo.Cliente,
                LibroId = prestamo.LibroId,
                Libro = prestamo.Libro
            };

            return View(prestamoVm);
        }

        [HttpPost]
        public IActionResult Editar(PrestamoVm prestamo)
        {
            if (ModelState.IsValid)
            {
                var prestamoExistente = _context.Prestamo.FirstOrDefault(p => p.PrestamoId == prestamo.Id);
                if (prestamoExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    prestamoExistente.ClienteId = prestamo.ClienteId;
                    prestamoExistente.LibroId = prestamo.LibroId;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar el préstamo.");
                    _logger.LogError(ex, "Error al editar el préstamo");
                }
            }

            return View(prestamo);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid prestamoId)
        {
            var prestamo = _context.Prestamo.FirstOrDefault(p => p.PrestamoId == prestamoId);
            if (prestamo == null)
            {
                return NotFound();
            }

            try
            {
                _context.Prestamo.Remove(prestamo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el préstamo.");
                _logger.LogError(ex, "Error al eliminar el préstamo");
                return View("Index");
            }
        }
    }
}
