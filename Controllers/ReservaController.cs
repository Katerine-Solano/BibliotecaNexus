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
    public class ReservaController : Controller
    {
        private readonly ILogger<ReservaController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public ReservaController(BibliotecaNexusDbContext context, ILogger<ReservaController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var reservas = _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Libro)
                .ProjectToType<ReservaVm>()
                .ToList();
            return View(reservas);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(ReservaVm reserva)
        {
            if (ModelState.IsValid)
            {
                var nuevaReserva = new Reserva
                {
                    ReservaId = Guid.NewGuid(),
                    ClienteId = reserva.ClienteId,
                    LibroId = reserva.LibroId
                };

                try
                {
                    _context.Reserva.Add(nuevaReserva);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar la nueva reserva.");
                    _logger.LogError(ex, "Error al agregar una nueva reserva");
                }
            }

            return View(reserva);
        }

        [HttpGet]
        public IActionResult Editar(Guid reservaId)
        {
            var reserva = _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Libro)
                .FirstOrDefault(r => r.ReservaId == reservaId);
            if (reserva == null)
            {
                return NotFound();
            }

            var reservaVm = new ReservaVm
            {
                Id = reserva.ReservaId,
                ClienteId = reserva.ClienteId,
                Cliente = reserva.Cliente,
                LibroId = reserva.LibroId,
                Libro = reserva.Libro
            };

            return View(reservaVm);
        }

        [HttpPost]
        public IActionResult Editar(ReservaVm reserva)
        {
            if (ModelState.IsValid)
            {
                var reservaExistente = _context.Reserva.FirstOrDefault(r => r.ReservaId == reserva.Id);
                if (reservaExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    reservaExistente.ClienteId = reserva.ClienteId;
                    reservaExistente.LibroId = reserva.LibroId;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar la reserva.");
                    _logger.LogError(ex, "Error al editar la reserva");
                }
            }

            return View(reserva);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid reservaId)
        {
            var reserva = _context.Reserva.FirstOrDefault(r => r.ReservaId == reservaId);
            if (reserva == null)
            {
                return NotFound();
            }

            try
            {
                _context.Reserva.Remove(reserva);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar la reserva.");
                _logger.LogError(ex, "Error al eliminar la reserva");
                return View("Index");
            }
        }
    }
}
