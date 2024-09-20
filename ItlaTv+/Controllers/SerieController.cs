using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private async Task LoadViewBagData()
        {
            var productoras = await _context.Productoras
                                             .Select(p => new { p.Id, p.Name })
                                             .ToListAsync();

            var generos = await _context.Generos
                                        .Select(g => new { g.Id, g.Name })
                                        .ToListAsync();

            ViewBag.ProductoraList = new SelectList(productoras, "Id", "Name");
            ViewBag.GeneroList = new SelectList(generos, "Id", "Name");
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

        public IActionResult Create()
        {
            // Cargar las productoras y los generos desde la base de datos
            var productoras = _context.Productoras
                                       .Select(p => new { p.Id, p.Name })
                                       .ToList();

            var generos = _context.Generos
                                  .Select(g => new { g.Id, g.Name })
                                  .ToList();

            // Pasar las listas a la vista
            ViewBag.ProductoraList = new SelectList(productoras, "Id", "Name");
            ViewBag.GeneroList = new SelectList(generos, "Id", "Name");

            return View("SalveSerie", new SalveSerietViewModel()); 
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(SalveSerietViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Cargar de nuevo las listas de productoras y géneros
                await LoadViewBagData();
                return View("SalveSerie", model);
            }

            await _serie.CreateSerie(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serie = await _serie.GetByIdSaveViewModel(id);

            
            var productoras = _context.Productoras
                                       .Select(p => new { p.Id, p.Name })
                                       .ToList();

            var generos = _context.Generos
                                  .Select(g => new { g.Id, g.Name })
                                  .ToList();

           
            ViewBag.ProductoraList = new SelectList(productoras, "Id", "Name");
            ViewBag.GeneroList = new SelectList(generos, "Id", "Name");

            return View("SalveSerie", serie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SalveSerietViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Cargar de nuevo las listas de productoras y géneros
                await LoadViewBagData();
                return View("SalveSerie", vm);
            }

            await _serie.UpdateSerie(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int id)
        {
            var serie = await _serie.GetByIdSaveViewModel(id);

            return View(serie);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _serie.Delete(id);

            return RedirectToRoute(new { controller = "Serie", action = "Index" });
        }

    }
}
