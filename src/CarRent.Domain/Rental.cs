using CarRent.Domain.Base;

namespace CarRent.Domain
{
    /// <summary>
    /// Representation of individual car rental in the system
    /// </summary>
    public class Rental : BaseModel
    {
        #region Foreign keys
        /// <summary>
        /// Id of user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id of car
        /// </summary>
        public int CarId { get; set; }
        #endregion

        /// <summary>
        /// Date and time on which the rental started
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Date and time on which the rental ended
        /// </summary>
        public DateTimeOffset To { get; set; }

        #region Parents
        /// <summary>
        /// Whole user object
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Whole car object
        /// </summary>
        public Car Car { get; set; } = null!;
        #endregion
    }
}
