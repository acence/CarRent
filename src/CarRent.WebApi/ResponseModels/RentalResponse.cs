namespace CarRent.WebApi.ResponseModels
{
    public class RentalResponse
    {
        public UserResponse User { get; set; } = null!;
        public CarResponse Car { get; set; } = null!;
        public DateTimeOffset Date { get; set; }
    }
}
