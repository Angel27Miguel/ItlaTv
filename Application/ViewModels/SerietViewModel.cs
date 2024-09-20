using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class SerietViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagenPortada { get; set; }
        public string EnlaceVideo { get; set; }
        public string ProductoraName { get; set; }
        public string GeneroPrimarioName { get; set; }
        public string GeneroSecundarioName { get; set; }
        public int ProductoraId { get; set; }
        public int GeneroPrimarioId { get; set; }
        public int GeneroSecundarioId { get; set; }
    }
}
