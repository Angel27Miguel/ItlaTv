

using Database.Common;

namespace Database.Entities
{
    public class Productora : BaseEntity
    {
        public ICollection<Series>? Series { get; set; }
    }
}
