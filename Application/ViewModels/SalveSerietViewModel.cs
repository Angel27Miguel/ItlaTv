
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class SalveSerietViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre de la serie")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar una imagen de portada de la serie")]
        public string ImagenPortada { get; set; }

        [Required(ErrorMessage = "Debe colocar el enlace del video de YouTube relacionado con la serie")]
        public string EnlaceVideo { get; set; }

        [Required(ErrorMessage = "Debe colocar la productora de la serie")]
        public int ProductoraId { get; set; }

        [Required(ErrorMessage = "Debe colocar el genero primario de la serie")]
        public int GeneroPrimarioId { get; set; }
        public int GeneroSecundarioId { get; set; }
    }
}
