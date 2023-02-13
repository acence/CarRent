namespace CarRent.WebApi.Models.Request.Rentals
{
    public class CreateRentalRequest
    {
        /// <summary>
        /// User Id for rental agreement
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Car Id for rental agreement
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Date and time for rent start
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Date and time for rent end
        /// </summary>
        public DateTimeOffset To { get; set; }
    }
}
