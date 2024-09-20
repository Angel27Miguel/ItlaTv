using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class GeneroService
    {
        private readonly GeneroRepository _repository;

        public GeneroService(ApplicationContext context)
        {
            _repository = new(context);
        }

        public async Task<List<GeneroViewModel>> GetAllViewModel()
        {
            var generoList = await _repository.GetAll().ToListAsync();

            return generoList.Select(genero => new GeneroViewModel
            {
                Id = genero.Id,
                Name = genero.Name,
            }).ToList();
        }

        public async Task CreateGenero(SalveGeneroViewModel vm)
        {
            Genero genero = new Genero();
            genero.Name = vm.Name;

            await _repository.AddAsync(genero);
        }

        public async Task<SalveGeneroViewModel> GetByIdSaveViewModel(int id)
        {
            var genero = await _repository.GetByIdAsync(id);

            SalveGeneroViewModel vm = new();
            vm.Id = id;
            vm.Name = genero.Name;

            return vm;
        }

        public async Task UpdateGenero(SalveGeneroViewModel vm)
        {
            var genero = await _repository.GetByIdAsync(vm.Id);
            if (genero != null)
            {
                genero.Name = vm.Name;

                await _repository.UpdateAsync(genero);
            }
        }

        public async Task Delete(int id)
        {
            var genero = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(genero);
        }
    }
}
