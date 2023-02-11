using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using MediatR;

namespace CarRent.Application.UseCases.Cars.Handlers
{
    public class GetAllCars : IRequestHandler<GetAllCars.Query, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCars(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<IEnumerable<Car>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetAllAsync(request.Make, request.Model, request.UniqueId);
        }

        public class Query : IRequest<IEnumerable<Car>>
        {
            public string? Make { get; set; }
            public string? Model { get; set; }
            public string? UniqueId { get; set; }
        }
    }
}
