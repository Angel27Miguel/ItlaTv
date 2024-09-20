using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ProductoraViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre de la productora")]
        public string Name { get; set; }
    }
}
