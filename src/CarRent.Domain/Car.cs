using CarRent.Domain.Base;

namespace CarRent.Domain
{
    /// <summary>
    /// Car that is being rented
    /// </summary>
    public class Car : BaseModel
    {
        /// <summary>
        /// Car manufacturer
        /// </summary>
        public string Make { get; set; } = null!;

        /// <summary>
        /// Car model
        /// </summary>
        public string Model { get; set; } = null!;

        /// <summary>
        /// Car unique Id
        /// </summary>
        public string UniqueId { get; set; } = null!;

        #region Children
        /// <summary>
        /// List of past and future rentals of cars
        /// </summary>
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        #endregion
    }
}
