namespace CarRent.WebApi.ResponseModels
{
    public class CarResponse
    {
        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string UniqueId { get; set; } = null!;
    }
}
