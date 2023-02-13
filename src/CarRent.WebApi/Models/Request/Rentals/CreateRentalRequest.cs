using System.ComponentModel.DataAnnotations;

namespace CarRent.WebApi.Models.Request.Rentals
{
    public class CreateRentalRequest
    {
        /// <summary>
        /// User Id for rental agreement
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Car Id for rental agreement
        /// </summary>
        [Required]
        public int CarId { get; set; }

        /// <summary>
        /// Date and time for rent start
        /// </summary>
        [Required]
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Date and time for rent end
        /// </summary>
        [Required]
        public DateTimeOffset To { get; set; }
    }
}
