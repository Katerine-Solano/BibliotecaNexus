using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BibliotecaNexus.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILogger<LibroController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public LibroController(BibliotecaNexusDbContext context, ILogger<LibroController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var libros = _context.Libro
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .ProjectToType<LibroVm>()
                .ToList();
            return View(libros);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(LibroVm libro)
        {
            if (ModelState.IsValid)
            {
                var nuevoLibro = new Libro
                {
                    LibroId = Guid.NewGuid(),
                    Titulo = libro.Titulo,
                    // Agregar otros campos necesarios
                };

                try
                {
                    _context.Libro.Add(nuevoLibro);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar el nuevo libro.");
                    _logger.LogError(ex, "Error al agregar un nuevo libro");
                }
            }

            return View(libro);
        }

        [HttpGet]
        public IActionResult Editar(Guid libroId)
        {
            var libro = _context.Libro
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefault(l => l.LibroId == libroId);
            if (libro == null)
            {
                return NotFound();
            }

            var libroVm = new LibroVm
            {
                LibroId = libro.LibroId,
                Titulo = libro.Titulo,
                // Agregar otros campos necesarios
            };

            return View(libroVm);
        }

        [HttpPost]
        public IActionResult Editar(LibroVm libro)
        {
            if (ModelState.IsValid)
            {
                var libroExistente = _context.Libro.FirstOrDefault(l => l.LibroId == libro.LibroId);
                if (libroExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    libroExistente.Titulo = libro.Titulo;
                    // Actualizar otros campos necesarios
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar el libro.");
                    _logger.LogError(ex, "Error al editar el libro");
                }
            }

            return View(libro);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid libroId)
        {
            var libro = _context.Libro.FirstOrDefault(l => l.LibroId == libroId);
            if (libro == null)
            {
                return NotFound();
            }

            try
            {
                _context.Libro.Remove(libro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el libro.");
                _logger.LogError(ex, "Error al eliminar el libro");
                return View("Index");
            }
        }
    }
}
