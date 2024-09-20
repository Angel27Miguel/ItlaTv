﻿

using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
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

        public async Task<SerietViewModel> GetByIdSaveViewModel(int id) 
        {
            var serie = await _repository.GetByIdAsync(id);

            SerietViewModel vm = new();
            vm.Id = id;  
            vm.Name = serie.Name;
            vm.ImagenPortada = serie.ImagenPortada;
            vm.EnlaceVideo = serie.EnlaceVideo;
            vm.ProductoraId = serie.ProductoraId;
            vm.GeneroPrimarioId= serie.GeneroPrimarioId;
            vm.GeneroSecundarioId = serie.GeneroSecundarioId;
            
            return vm;
        }
    }
}
