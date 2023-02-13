namespace CarRent.WebApi.Models.Response
{
    public class UserResponse
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User full name
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
