using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaNexus.Controllers
{
    public class AutorizacionController : Controller
    {
        private readonly BibliotecaNexusDbContext _context;

        public AutorizacionController(BibliotecaNexusDbContext context)
        {
            _context = context;
        }

        // GET: Autorizacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autorizaciones.ToListAsync());
        }

        // GET: Autorizacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorizacion = await _context.Autorizaciones
                .FirstOrDefaultAsync(m => m.AutorizacionId == id);
            if (autorizacion == null)
            {
                return NotFound();
            }

            return View(autorizacion);
        }

        // GET: Autorizacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autorizacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutorizacionId,Modulo,Permiso")] Autorizacion autorizacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autorizacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autorizacion);
        }

        // GET: Autorizacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorizacion = await _context.Autorizaciones.FindAsync(id);
            if (autorizacion == null)
            {
                return NotFound();
            }
            return View(autorizacion);
        }

        // POST: Autorizacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutorizacionId,Modulo,Permiso")] Autorizacion autorizacion)
        {
            if (id != autorizacion.AutorizacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autorizacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorizacionExists(autorizacion.AutorizacionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(autorizacion);
        }

        // GET: Autorizacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorizacion = await _context.Autorizaciones
                .FirstOrDefaultAsync(m => m.AutorizacionId == id);
            if (autorizacion == null)
            {
                return NotFound();
            }

            return View(autorizacion);
        }

        // POST: Autorizacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autorizacion = await _context.Autorizaciones.FindAsync(id);
            _context.Autorizaciones.Remove(autorizacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorizacionExists(int id)
        {
            return _context.Autorizaciones.Any(e => e.AutorizacionId == id);
        }
    }
}