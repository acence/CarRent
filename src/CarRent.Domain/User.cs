using CarRent.Domain.Base;

namespace CarRent.Domain
{
    /// <summary>
    /// User that rents cars
    /// </summary>
    public class User : BaseModel
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; } = null!;

        #region Children
        /// <summary>
        /// List of past and future rentals of cars
        /// </summary>
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        #endregion
    }
}