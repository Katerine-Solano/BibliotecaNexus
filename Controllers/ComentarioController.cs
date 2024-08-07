using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BibliotecaNexus.Controllers
{
    public class ComentarioController : Controller
    {
        private readonly ILogger<ComentarioController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public ComentarioController(BibliotecaNexusDbContext context, ILogger<ComentarioController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var comentarios = _context.Comentario
                .Include(c => c.Cliente)
                .Include(c => c.Libro)
                .ProjectToType<ComentarioVm>()
                .ToList();
            return View(comentarios);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(ComentarioVm comentario)
        {
            if (ModelState.IsValid)
            {
                var nuevoComentario = new Comentario
                {
                    ComentarioId = Guid.NewGuid(),
                    ClienteId = comentario.ClienteId,
                    LibroId = comentario.LibroId
                };

                try
                {
                    _context.Comentario.Add(nuevoComentario);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar el nuevo comentario.");
                    _logger.LogError(ex, "Error al agregar un nuevo comentario");
                }
            }

            return View(comentario);
        }

        [HttpGet]
        public IActionResult Editar(Guid comentarioId)
        {
            var comentario = _context.Comentario
                .Include(c => c.Cliente)
                .Include(c => c.Libro)
                .FirstOrDefault(c => c.ComentarioId == comentarioId);
            if (comentario == null)
            {
                return NotFound();
            }

            var comentarioVm = new ComentarioVm
            {
                ComentarioId = comentario.ComentarioId,
                ClienteId = comentario.ClienteId,
                Cliente = comentario.Cliente,
                LibroId = comentario.LibroId,
                Libro = comentario.Libro
            };

            return View(comentarioVm);
        }

        [HttpPost]
        public IActionResult Editar(ComentarioVm comentario)
        {
            if (ModelState.IsValid)
            {
                var comentarioExistente = _context.Comentario.FirstOrDefault(c => c.ComentarioId == comentario.ComentarioId);
                if (comentarioExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    comentarioExistente.ClienteId = comentario.ClienteId;
                    comentarioExistente.LibroId = comentario.LibroId;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar el comentario.");
                    _logger.LogError(ex, "Error al editar el comentario");
                }
            }

            return View(comentario);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid comentarioId)
        {
            var comentario = _context.Comentario.FirstOrDefault(c => c.ComentarioId == comentarioId);
            if (comentario == null)
            {
                return NotFound();
            }

            try
            {
                _context.Comentario.Remove(comentario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el comentario.");
                _logger.LogError(ex, "Error al eliminar el comentario");
                return View("Index");
            }
        }
    }
}
