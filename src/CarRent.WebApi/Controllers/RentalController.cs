using AutoMapper;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.WebApi.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.WebApi.Controllers
{
    [Route("api/v1/rental")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RentalController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("upcoming")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RentalResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<RentalResponse>> GetUpcoming(int userId)
        {
            var result = await _mediator.Send(new GetUpcomingRentals.Query { UserId = userId });
            return _mapper.Map<IEnumerable<RentalResponse>>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<RentalResponse> CreateRental(CreateRental.Command command)
        {
            var result = await _mediator.Send(command);
            return _mapper.Map<RentalResponse>(result);
        }
    }
}
