namespace CarRent.WebApi.Models.Request.Car
{
    public class CreateNewCarRequest
    {
        /// <summary>
        /// Car Make
        /// </summary>
        public string Make { get; set; } = null!;

        /// <summary>
        /// Car Model
        /// </summary>
        public string Model { get; set; } = null!;

        /// <summary>
        /// Car unique Id
        /// </summary>
        public string UniqueId { get; set; } = null!;
    }
}
