using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using MediatR;

namespace CarRent.Application.UseCases.Rentals.Handlers
{
    public class GetAvailableCars : IRequestHandler<GetAvailableCars.Query, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAvailableCars(ICarRepository carRepository)
        {
            ArgumentNullException.ThrowIfNull(carRepository);

            _carRepository = carRepository;
        }
        public async Task<IEnumerable<Car>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetAvailableCarsAsync(request.Date);
        }

        public class Query : IRequest<IEnumerable<Car>>
        {
            public DateOnly Date { get; set; }
        }
    }
}
