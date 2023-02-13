namespace CarRent.WebApi.Models.Response
{
    public class RentalResponse
    {
        /// <summary>
        /// Rental agreement Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date and time of rental start
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Date and time of rental end
        /// </summary>
        public DateTimeOffset To { get; set; }

        /// <summary>
        /// Details for the rented car
        /// </summary>
        public UserResponse User { get; set; } = null!;

        /// <summary>
        /// Details for the renter
        /// </summary>
        public CarResponse Car { get; set; } = null!;
    }
}
