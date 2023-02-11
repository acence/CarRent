using CarRent.Domain.Base;

namespace CarRent.Domain
{
    public class Rental : BaseModel
    {
        public int UserId { get; set; }
        public int CarId { get; set; }

        public DateOnly RentDate { get; set; }

        public User User { get; set; } = null!;
        public Car Car { get; set; } = null!;
    }
}
