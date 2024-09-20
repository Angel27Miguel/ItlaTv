

using Database.Common;

namespace Database.Entities
{
    public class Genero : BaseEntity
    {
        public ICollection<Series> Series { get; set; } = new List<Series>();
    }

}
