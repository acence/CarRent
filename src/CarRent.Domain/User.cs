using CarRent.Domain.Base;

namespace CarRent.Domain
{
    public class User : BaseModel
    {
        public string Name { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}