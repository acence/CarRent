namespace CarRent.WebApi.Models.Response
{
    public class CarResponse
    {
        /// <summary>
        /// Car Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Car make
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
