using Application.Services;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItlaTv_.Controllers
{
    public class HomeController : Controller
    {
        private readonly SerieService _serie;
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
            _serie = new(context);
        }

        public async Task<IActionResult> Index(string searchText, int? productoraId, int? generoId)
        {
            // Obtener las productoras y géneros desde la base de datos
            ViewBag.Productoras = await _context.Productoras.ToListAsync();
            ViewBag.Generos = await _context.Generos.ToListAsync();

            // Mantener los valores seleccionados para los filtros
            ViewBag.SelectedProductoraId = productoraId;
            ViewBag.SelectedGeneroId = generoId;
            ViewBag.SearchText = searchText;

            var serie = await _serie.GetAllViewModel();

            if (!string.IsNullOrEmpty(searchText))
            {
                serie = serie.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (productoraId.HasValue)
            {
                serie = serie.Where(p => p.ProductoraId == productoraId).ToList();
            }

            if (generoId.HasValue)
            {
                serie = serie.Where(p => p.GeneroPrimarioId == generoId || p.GeneroSecundarioId == generoId).ToList();
            }

            return View(serie);
        }



    }
}
