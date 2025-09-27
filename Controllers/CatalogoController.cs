using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PC2.Data;
using PC2.Models;

namespace PC2.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;

        public CatalogoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string ciudad, TipoInmueble? tipo, decimal? precioMin, decimal? precioMax, int? dormitorios, int pageNumber = 1)
        {
            int pageSize = 5; // Paginación simple de 5 inmuebles por página

            var inmueblesQuery = _context.Inmuebles.AsQueryable();

            // Filtros
            if (!string.IsNullOrEmpty(ciudad))
            {
                inmueblesQuery = inmueblesQuery.Where(i => i.Ciudad.Contains(ciudad));
            }

            if (tipo.HasValue)
            {
                inmueblesQuery = inmueblesQuery.Where(i => i.Tipo == tipo);
            }

            if (precioMin.HasValue)
            {
                inmueblesQuery = inmueblesQuery.Where(i => i.Precio >= precioMin);
            }

            if (precioMax.HasValue)
            {
                inmueblesQuery = inmueblesQuery.Where(i => i.Precio <= precioMax);
            }

            if (dormitorios.HasValue)
            {
                inmueblesQuery = inmueblesQuery.Where(i => i.Dormitorios >= dormitorios);
            }

            // Paginación
            var totalInmuebles = await inmueblesQuery.CountAsync();
            var inmuebles = await inmueblesQuery
                .Where(i => i.Activo) // Solo inmuebles activos
                .OrderBy(i => i.Codigo) // O puedes cambiar el orden
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Crear ViewModel para enviar datos
            var viewModel = new CatalogoViewModel
            {
                Inmuebles = inmuebles,
                TotalInmuebles = totalInmuebles,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Ciudad = ciudad,
                Tipo = tipo,
                PrecioMin = precioMin,
                PrecioMax = precioMax,
                Dormitorios = dormitorios
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Detalle(int id)
        {
            var inmueble = await _context.Inmuebles
                .FirstOrDefaultAsync(i => i.Id == id && i.Activo);

            if (inmueble == null)
            {
                return NotFound();
            }

            return View(inmueble);
        }

        // Agendar Visita (Formulario)
        [HttpPost]
        public IActionResult AgendarVisita(int inmuebleId)
        {
            // Redirigir a la vista de agendar visita (puedes agregar la lógica aquí)
            return RedirectToAction("Create", "Visita", new { inmuebleId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }


}