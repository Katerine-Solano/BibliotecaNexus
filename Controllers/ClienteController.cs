using BibliotecaNexus.Data.Domain;
using BibliotecaNexus.Data.Domain.Entidades;
using BibliotecaNexus.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BibliotecaNexus.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly BibliotecaNexusDbContext _context;

        public ClienteController(BibliotecaNexusDbContext context, ILogger<ClienteController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var clientes = _context.Cliente.Where(c => !c.Eliminado).ProjectToType<ClienteVm>().ToList();

            return View(clientes);
        }

        [HttpPost]
        public IActionResult Insertar(ClienteVm cliente)
        {
            if (ModelState.IsValid)
            {
                var nuevaCliente = new Cliente
                {
                    Nombre = cliente.Nombre,
                    Direccion = cliente.Direccion,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono,
                    Eliminado = false,
                    ClienteId = Guid.NewGuid(),
                };

                try
                {
                    _context.Cliente.Add(nuevaCliente);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar agregar la nueva cliente.");
                    _logger.LogError(ex, "Error al agregar un nuevo cliente");
                }
            }

            return View(cliente);
        }

        [HttpGet]
        public IActionResult Editar(Guid clienteId)
        {
            var cliente = _context.Cliente.FirstOrDefault(c => c.ClienteId == clienteId);
            if (cliente == null)
            {
                return NotFound();
            }

            var clienteVm = new ClienteVm
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Direccion = cliente.Direccion,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };

            return View(clienteVm);
        }


        [HttpPost]
        public IActionResult Editar(ClienteVm cliente)
        {
            if (ModelState.IsValid)
            {
                var clienteExistente = _context.Cliente.FirstOrDefault(c => c.ClienteId == cliente.ClienteId);
                if (clienteExistente == null)
                {
                    return NotFound();
                }

                try
                {
                    clienteExistente.Nombre = cliente.Nombre;
                    clienteExistente.Direccion = cliente.Direccion;
                    clienteExistente.Email = cliente.Email;
                    clienteExistente.Telefono = cliente.Telefono;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar el cliente.");
                    _logger.LogError(ex, "Error al editar el cliente");
                }
            }

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Eliminar(Guid clienteId)
        {
            var cliente = _context.Cliente.FirstOrDefault(n => n.ClienteId == clienteId);
            if (cliente == null)
            {
                return NotFound();
            }

            try
            {

                cliente.Eliminado = true;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar añ cliente.");
                _logger.LogError(ex, "Error al eliminar al cliente");
                return View("Index");
            }
        }
    }
}