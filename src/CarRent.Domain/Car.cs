using CarRent.Domain.Base;

namespace CarRent.Domain
{
    public class Car : BaseModel
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string UniqueId { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
