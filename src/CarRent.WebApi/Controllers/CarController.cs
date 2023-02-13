using AutoMapper;
using CarRent.Application.Behaviours;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Domain;
using CarRent.WebApi.Controllers.Base;
using CarRent.WebApi.Models.Request.Car;
using CarRent.WebApi.Models.Response;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRent.WebApi.Controllers
{
    /// <summary>
    /// Rest API controller for car specific functions
    /// </summary>
    [Route("api/v1/cars")]
    [ApiController]
    public class CarController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarController(IMediator mediator, IMapper mapper, ILogger<CarController> logger) : base(logger)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all cars in system, filter if values are provided
        /// </summary>
        /// <param name="make">Car make</param>
        /// <param name="model">Car model</param>
        /// <param name="uniqueId">Car unique id</param>
        /// <returns>List of cars in system</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> Get(string? make, string? model, string? uniqueId)
        {
            var query = new GetAllCars.Query { Make = make, Model = model, UniqueId = uniqueId };
            return await ProcessResponse(async () =>
            {
                var result = await _mediator.Send(query);
                return _mapper.Map<IEnumerable<CarResponse>>(result);
            });
        }

        /// <summary>
        /// Creates a new car in the system
        /// </summary>
        /// <param name="request">Request model for creating a new car</param>
        /// <returns>Record for created car</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CarResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> Post([FromBody] CreateNewCarRequest request)
        {
            var command = _mapper.Map<CreateNewCar.Command>(request);
            return await ProcessResponse(async () =>
            {
                var result = await _mediator.Send(command);
                return _mapper.Map<CarResponse>(result);
            });
        }

        /// <summary>
        /// Updates an existing car in system
        /// </summary>
        /// <param name="id">Id of the car, provided in route as per Rest guidelines</param>
        /// <param name="request">The rest of the data for updating the car entry</param>
        /// <returns>Record for updated car</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCarRequest request)
        {
            var command = _mapper.Map<UpdateCar.Command>(request);
            command.Id = id;

            return await ProcessResponse(async () =>
            {
                var result = await _mediator.Send(command);
                return _mapper.Map<CarResponse>(result);
            });
        }

        /// <summary>
        /// Deletes a car from the system
        /// </summary>
        /// <param name="id">Id of the car to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCar.Command { Id = id };
            return await ProcessResponse(async () =>
            {
                var result = await _mediator.Send(command);
            });
        }
    }
}
