using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BibliotecaNexus.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public CategoriaController(BibliotecaNexusDbContext context, ILogger<CategoriaController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var categorias = _context.Categoria
                .ProjectToType<CategoriaVm>()
                .ToList();
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(CategoriaVm categoria)
        {
            if (ModelState.IsValid)
            {
                var nuevaCategoria = new Categoria
                {
                    CategoriaId = Guid.NewGuid(),
                    Nombre = categoria.Nombre
                };

                try
                {
                    _context.Categoria.Add(nuevaCategoria);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar la nueva categoría.");
                    _logger.LogError(ex, "Error al agregar una nueva categoría");
                }
            }

            return View(categoria);
        }

        [HttpGet]
        public IActionResult Editar(Guid categoriaId)
        {
            var categoria = _context.Categoria.FirstOrDefault(c => c.CategoriaId == categoriaId);
            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaVm = new CategoriaVm
            {
                CategoriaId = categoria.CategoriaId,
                Nombre = categoria.Nombre
            };

            return View(categoriaVm);
        }

        [HttpPost]
        public IActionResult Editar(CategoriaVm categoria)
        {
            if (ModelState.IsValid)
            {
                var categoriaExistente = _context.Categoria.FirstOrDefault(c => c.CategoriaId == categoria.CategoriaId);
                if (categoriaExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    categoriaExistente.Nombre = categoria.Nombre;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar la categoría.");
                    _logger.LogError(ex, "Error al editar la categoría");
                }
            }

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid categoriaId)
        {
            var categoria = _context.Categoria.FirstOrDefault(c => c.CategoriaId == categoriaId);
            if (categoria == null)
            {
                return NotFound();
            }

            try
            {
                _context.Categoria.Remove(categoria);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar la categoría.");
                _logger.LogError(ex, "Error al eliminar la categoría");
                return View("Index");
            }
        }
    }
}
