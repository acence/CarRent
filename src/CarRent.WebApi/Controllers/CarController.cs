using AutoMapper;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Domain;
using CarRent.WebApi.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRent.WebApi.Controllers
{
    [Route("api/v1/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<CarResponse>> Get(string? make, string? model, string? uniqueId)
        {
            var result = await _mediator.Send(new GetAllCars.Query { Make = make, Model = model, UniqueId = uniqueId });
            return _mapper.Map<IEnumerable<CarResponse>>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CarResponse> Post([FromBody] CreateNewCar.Command command)
        {
            var result = await _mediator.Send(command);
            return _mapper.Map<CarResponse>(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CarResponse> Put(int id, [FromBody] UpdateCar.Command command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return _mapper.Map<CarResponse>(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete(int id)
        {
            await _mediator.Send(new DeleteCar.Command { Id = id });
        }
    }
}
