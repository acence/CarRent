using AutoMapper;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.WebApi.Models.Request.Rentals;
using CarRent.WebApi.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CarRent.WebApi.Extensions;

namespace CarRent.WebApi.Controllers
{
    /// <summary>
    /// Rest API controller for renting cars, seeing availability and rented cars
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/rental")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RentalController(IMediator mediator, IMapper mapper, ILogger<RentalController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets upcoming rentals for user, either for today onward or from a specific date
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="from">Date from which the rentals start</param>
        /// <returns>List of rental agreements for cars for specific user</returns>
        [HttpGet]
        [Route("{userId}/upcoming")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RentalResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> GetUpcoming(int userId, DateTimeOffset? from)
        {
            from = from ?? DateTimeOffset.Now;
            var query = new GetUpcomingRentals.Query { From = from.Value, UserId = userId };

            return await _mediator.SendAndProcessResponseAsync<GetUpcomingRentals.Query, IEnumerable<RentalResponse>>(_mapper, query);
        }

        /// <summary>
        /// Get available cars from specific date onwards
        /// </summary>
        /// <param name="from">Start date and time for filtering</param>
        /// <param name="to">End date and time for filtering</param>
        /// <returns>List of cars available for rent in specified interval</returns>
        [HttpGet]
        [Route("available-cars")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> GetAvailableCars(DateTimeOffset from, DateTimeOffset? to)
        {
            var query = new GetAvailableCars.Query { From = from, To = to };

            return await _mediator.SendAndProcessResponseAsync<GetAvailableCars.Query, IEnumerable<CarResponse>>(_mapper, query);
        }

        /// <summary>
        /// Creates a new rental agreement
        /// </summary>
        /// <param name="request">Request for creating rental agreement</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RentalResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> CreateRental(CreateRentalRequest request)
        {
            var command = _mapper.Map<CreateRental.Command>(request);

            return await _mediator.SendAndProcessResponseAsync<CreateRental.Command, RentalResponse>(_mapper, command);
        }
    }
}
