using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PC2.Data;
using PC2.Models;
// Acción GET para mostrar el formulario de agendar visita

namespace PC2.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        public CatalogoController(ApplicationDbContext context, UserManager<Usuario> userManager, ILogger<CatalogoController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
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
                                .Include(i => i.Reservas) // Incluir reservas si es necesario
                                .FirstOrDefaultAsync(i => i.Id == id);

            if (inmueble == null)
            {
                return NotFound();
            }
            InmuebleVisitaViewModel model = new InmuebleVisitaViewModel();
            // Verificar si existe una reserva activa
            bool tieneReservaActiva = inmueble.Reservas.Any(r => r.FechaExpiracion > DateTime.Now);
            inmueble.ReservaActiva = tieneReservaActiva;
            model.Inmueble = inmueble;
            // Pasar el ViewModel a la vista
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AgendarVisita(VisitaViewModel model2)
        {
            _logger.LogInformation("Buscando Inmueble con ID: {InmuebleId}", model2.InmuebleId);
            var inmueble = await _context.Inmuebles
                .FirstOrDefaultAsync(i => i.Id == model2.InmuebleId);

            // Obtener inmuebleId del formulario
            if (!int.TryParse(Request.Form["inmuebleId"], out var inmuebleId))
            {
                _logger.LogWarning("ID de inmueble no válido.");
                ModelState.AddModelError("", "El ID del inmueble no es válido.");
                return View("Detalle", inmueble);
            }

            // Cargar el Inmueble desde la base de datos

            if (inmueble == null)
            {
                _logger.LogWarning("Inmueble con ID {InmuebleId} no encontrado.", inmuebleId);
                ModelState.AddModelError("", "El inmueble no existe.");
                return View("Detalle", inmueble);
            }

            // Asignar el inmueble al model2o para la vista
            InmuebleVisitaViewModel model22 = new();
            // Inicializar propiedades si son null
            model22.Inmueble = inmueble;
            model22.FechaInicio = model2.FechaInicio;
            model22.FechaFin = model2.FechaFin;
            model22.Notas = model2.Notas;

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState no válido: {Errors}", string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return View("Detalle", model22);
            }
            TimeSpan horaInicioPermitida = new TimeSpan(8, 0, 0);  // 08:00
            TimeSpan horaFinPermitida = new TimeSpan(19, 0, 0);    // 19:00

            TimeSpan horaInicio = model22.FechaInicio.Value.TimeOfDay;
            TimeSpan horaFin = model22.FechaFin.Value.TimeOfDay;

            if (horaInicio < horaInicioPermitida || horaFin > horaFinPermitida)
            {
                _logger.LogWarning("La visita está fuera del horario laboral (08:00–19:00).");
                ModelState.AddModelError("", "Las visitas deben programarse entre las 08:00 y las 19:00.");
                return View("Detalle", model22);
            }


            if (model22.FechaInicio >= model22.FechaFin)
            {
                _logger.LogWarning("Fecha de inicio no es anterior a la fecha de fin.");
                ModelState.AddModelError("", "La fecha de inicio debe ser anterior a la fecha de fin.");
                return View("Detalle", model22);
            }

            // Verificar visitas solapadas
            _logger.LogInformation("Verificando visitas solapadas para InmuebleId: {InmuebleId}", inmuebleId);
            bool hayVisitaSolapada = _context.Visitas
                .Any(v =>
                          v.FechaInicio < model22.FechaFin &&
                          v.FechaFin > model22.FechaInicio);

            if (hayVisitaSolapada)
            {
                _logger.LogWarning("Visita solapada encontrada.");
                ModelState.AddModelError("", "Ya existe una visita solapada en el intervalo de fechas.");
                return View("Detalle", model22);
            }

            // Crear la visita
            var visita = new Visita
            {
                InmuebleId = inmuebleId,
                FechaInicio = model22.FechaInicio,
                FechaFin = model22.FechaFin,
                Notas = model22.Notas,
                Estado = EstadoVisita.Solicitada
            };

            // Guardar la visita
            try
            {
                _logger.LogInformation("Agregando visita: {@Visita}", visita);
                _context.Visitas.Add(visita);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Visita guardada exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la visita.");
                ModelState.AddModelError("", $"Error al guardar la visita: {ex.Message}");
                return View("Detalle", model22);
            }

            TempData["SuccessMessage"] = "La visita ha sido agendada correctamente.";
            return RedirectToAction("Detalle", new { id = inmuebleId });
        }

        // Acción para Reservar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservar(int inmuebleId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "Debes estar autenticado para realizar una reserva.");
                return RedirectToAction("Detalle", new { id = inmuebleId });
            }

            // Verificar si ya existe una reserva activa
            var reservaActiva = await _context.Reservas
                .Where(r => r.InmuebleId == inmuebleId && r.FechaExpiracion > DateTime.Now)
                .FirstOrDefaultAsync();

            if (reservaActiva != null)
            {
                ModelState.AddModelError("", "Este inmueble ya tiene una reserva activa.");
                return RedirectToAction("Detalle", new { id = inmuebleId });
            }

            // Crear una nueva reserva
            var reserva = new Reserva
            {
                InmuebleId = inmuebleId,
                UsuarioId = _userManager.GetUserId(User),
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddHours(48)  // Expira en 48 horas
            };

            _context.Add(reserva);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reserva realizada con éxito.";
            return RedirectToAction("Detalle", new { id = inmuebleId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }


}