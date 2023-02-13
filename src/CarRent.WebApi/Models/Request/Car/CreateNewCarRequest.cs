using System.ComponentModel.DataAnnotations;

namespace CarRent.WebApi.Models.Request.Car
{
    public class CreateNewCarRequest
    {
        /// <summary>
        /// Car Make
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Make { get; set; } = null!;

        /// <summary>
        /// Car Model
        /// </summary>
        [Required]
        [MaxLength(120)]
        public string Model { get; set; } = null!;

        /// <summary>
        /// Car unique Id
        /// </summary>
        [Required]
        [MaxLength(13)]
        public string UniqueId { get; set; } = null!;
    }
}
