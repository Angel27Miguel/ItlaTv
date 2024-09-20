

using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class SerieService
    {
        private readonly SerieRepository _repository;
        private readonly ApplicationContext _context;

        public SerieService(ApplicationContext context)
        {
            _context = context;
            _repository = new(context);
        }

        public async Task<List<SerietViewModel>> GetAllViewModel()
        {
            var serieList = await _repository.GetAll()
                                             .Include(s => s.Productora)
                                             .Include(s => s.GeneroPrimario)
                                             .Include(s => s.GeneroSecundario)
                                             .ToListAsync();

            return serieList.Select(serie => new SerietViewModel
            {
                Id = serie.Id,
                Name = serie.Name,
                ImagenPortada = serie.ImagenPortada,
                EnlaceVideo = serie.EnlaceVideo,
                ProductoraName = serie.Productora?.Name,
                ProductoraId = serie.ProductoraId,
                GeneroPrimarioName = serie.GeneroPrimario?.Name,
                GeneroPrimarioId = serie.GeneroPrimarioId,
                GeneroSecundarioName = serie.GeneroSecundario?.Name,
                GeneroSecundarioId = serie.GeneroSecundarioId
            }).ToList();
        }

        public async Task CreateSerie(SalveSerietViewModel vm)
        {
            Series series = new Series();
            series.Name = vm.Name;
            series.ImagenPortada = vm.ImagenPortada;
            series.EnlaceVideo = vm.EnlaceVideo;
            series.ProductoraId = vm.ProductoraId;
            series.GeneroPrimarioId = vm.GeneroPrimarioId;
            series.GeneroSecundarioId = vm.GeneroSecundarioId;

            await _repository.AddAsync(series);

        }


        public async Task<SalveSerietViewModel> GetByIdSaveViewModel(int id) 
        {
            var serie = await _repository.GetByIdAsync(id);

            SalveSerietViewModel vm = new();
            vm.Id = id;  
            vm.Name = serie.Name;
            vm.ImagenPortada = serie.ImagenPortada;
            vm.EnlaceVideo = serie.EnlaceVideo;
            vm.ProductoraId = serie.ProductoraId;
            vm.GeneroPrimarioId= serie.GeneroPrimarioId;
            vm.GeneroSecundarioId = serie.GeneroSecundarioId;
            
            return vm;
        }

        public async Task UpdateSerie(SalveSerietViewModel vm)
        {
            var series = await _repository.GetByIdAsync(vm.Id);
            if (series != null)
            {
                series.Name = vm.Name;
                series.ImagenPortada = vm.ImagenPortada;
                series.EnlaceVideo = vm.EnlaceVideo;
                series.ProductoraId = vm.ProductoraId;
                series.GeneroPrimarioId = vm.GeneroPrimarioId;
                series.GeneroSecundarioId = vm.GeneroSecundarioId;

                await _repository.UpdateAsync(series);
            }
        }

        public async Task Delete(int id)
        {
            var serie = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(serie);
        }

    }
}
