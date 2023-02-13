using System.ComponentModel.DataAnnotations;

namespace CarRent.WebApi.Models.Response
{
    public class ServerErrorResponse
    {
        /// <summary>
        /// Exception message
        /// </summary>
        [Required]
        public string Message { get; set; } = null!;
    }
}
