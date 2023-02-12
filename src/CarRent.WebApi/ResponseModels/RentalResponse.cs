namespace CarRent.WebApi.ResponseModels
{
    public class RentalResponse
    {
        public int Id { get; set; }
        public UserResponse User { get; set; } = null!;
        public CarResponse Car { get; set; } = null!;
        public DateOnly Date { get; set; }
    }
}
