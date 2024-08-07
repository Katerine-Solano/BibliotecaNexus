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
    public class AutorController : Controller
    {
        private readonly ILogger<AutorController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public AutorController(BibliotecaNexusDbContext context, ILogger<AutorController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var autores = _context.Autore.ProjectToType<AutorVm>().ToList();
            return View(autores);
        }

        [HttpPost]
        public IActionResult Insertar(AutorVm autorVm)
        {
            if (ModelState.IsValid)
            {
                var nuevoAutor = new Autor
                {
                    AutorId = Guid.NewGuid(),
                    Nombre = autorVm.Nombre,
                    Apellido = autorVm.Apellido,
                    Telefono = autorVm.Telefono,
                    Cargo = autorVm.Cargo,
                    Libros = null // Inicializar con null o una lista vacía según corresponda
                };

                try
                {
                    _context.Autore.Add(nuevoAutor);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar el nuevo autor.");
                    _logger.LogError(ex, "Error al agregar un nuevo autor");
                }
            }

            return View(autorVm);
        }

        [HttpGet]
        public IActionResult Editar(Guid autorId)
        {
            var autor = _context.Autore.FirstOrDefault(a => a.AutorId == autorId);
            if (autor == null)
            {
                return NotFound();
            }

            var autorVm = autor.Adapt<AutorVm>();

            return View(autorVm);
        }

        [HttpPost]
        public IActionResult Editar(AutorVm autorVm)
        {
            if (ModelState.IsValid)
            {
                var autorExistente = _context.Autore.FirstOrDefault(a => a.AutorId == autorVm.AutorId);
                if (autorExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    autorExistente.Nombre = autorVm.Nombre;
                    autorExistente.Apellido = autorVm.Apellido;
                    autorExistente.Telefono = autorVm.Telefono;
                    autorExistente.Cargo = autorVm.Cargo;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar el autor.");
                    _logger.LogError(ex, "Error al editar el autor");
                }
            }

            return View(autorVm);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid autorId)
        {
            var autor = _context.Autore.FirstOrDefault(a => a.AutorId == autorId);
            if (autor == null)
            {
                return NotFound();
            }

            try
            {
                _context.Autore.Remove(autor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el autor.");
                _logger.LogError(ex, "Error al eliminar el autor");
                return View("Index");
            }
        }
    }
}
