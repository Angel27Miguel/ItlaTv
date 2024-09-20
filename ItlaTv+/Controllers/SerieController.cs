using Application.Services;
using Database.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItlaTv_.Controllers
{
    public class SerieController : Controller
    {
        private readonly SerieService _serie;
        private readonly ApplicationContext _context;

        public SerieController(ApplicationContext context)
        {
            _context = context;
            _serie = new(context);
        }

        public async Task<IActionResult> Index(string searchText, int? productoraId, int? generoId)
        {
            ViewBag.Productoras = await _context.Productoras.ToListAsync();
            ViewBag.Generos = await _context.Generos.ToListAsync();

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

        public async Task<ActionResult> Details(int id)
        {
            var serie = await _serie.GetByIdSaveViewModel(id);

            return View(serie);
        }

    }
}
