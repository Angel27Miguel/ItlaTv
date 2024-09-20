
using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProductoraService
    {
        private readonly ProductoraRepository _repository;

        public ProductoraService(ApplicationContext context)
        {
            _repository = new(context);
        }

        public async Task<List<ProductoraViewModel>> GetAllViewModel()
        {
            var productoraList = await _repository.GetAll().ToListAsync();

            return productoraList.Select(productora => new ProductoraViewModel
            {
                Id = productora.Id,
                Name = productora.Name,
            }).ToList();
        }

        public async Task CreateProductora(SalveProductoraViewModel vm)
        {
            Productora productora = new Productora();
            productora.Name = vm.Name;

            await _repository.AddAsync(productora);
        }

        public async Task<SalveProductoraViewModel> GetByIdSaveViewModel(int id)
        {
            var productora = await _repository.GetByIdAsync(id);

            SalveProductoraViewModel vm = new();
            vm.Id = id;
            vm.Name = productora.Name;

            return vm;
        }

        public async Task UpdateProductora(SalveProductoraViewModel vm)
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
            var productora = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(productora);
        }
    }
}
}
