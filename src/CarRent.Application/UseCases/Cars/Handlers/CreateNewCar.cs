using AutoMapper;
using CarRent.Application.Exceptions.CarExceptions;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using MediatR;

namespace CarRent.Application.UseCases.Cars.Handlers
{
    public class CreateNewCar: IRequestHandler<CreateNewCar.Command, Car>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CreateNewCar(ICarRepository carRepository, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(carRepository);
            ArgumentNullException.ThrowIfNull(mapper);

            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<Car> Handle(Command request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);

            var affectedResults = await _carRepository.Insert(car);
            if (affectedResults == 0)
            {
                throw new CarNotCreatedException();
            }

            return car;
        }

        public class Command : IRequest<Car>
        {
            public string Make { get; set; } = null!;
            public string Model { get; set; } = null!;
            public string UniqueId { get; set; } = null!;
        }
    }
}
