

using Database.Common;

namespace Database.Entities
{
    public class Series : BaseEntity
    {
        public string ImagenPortada { get; set; }
        public string EnlaceVideo { get; set; }

        public int ProductoraId { get; set; } // FK
        public int GeneroPrimarioId { get; set; } // FK para el género primario
        public int GeneroSecundarioId { get; set; } // FK para el género secundario

        // Navigation properties
        public Productora? Productora { get; set; }
        public Genero? GeneroPrimario { get; set; } // Relación con el género primario
        public Genero? GeneroSecundario { get; set; } // Relación con el género secundario
    }


}
