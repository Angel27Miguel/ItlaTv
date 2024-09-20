using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class GeneroViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del genero")]
        public string Name { get; set; }
    }
}
