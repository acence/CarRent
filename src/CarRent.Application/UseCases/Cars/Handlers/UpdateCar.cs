using AutoMapper;
using CarRent.Application.Exceptions;
using CarRent.Application.Exceptions.CarExceptions;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using MediatR;

namespace CarRent.Application.UseCases.Cars.Handlers
{
    public class UpdateCar : IRequestHandler<UpdateCar.Command, Car>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public UpdateCar(ICarRepository carRepository, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(carRepository);
            ArgumentNullException.ThrowIfNull(mapper);

            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<Car> Handle(Command request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);

            var affectedResults = await _carRepository.Update(car);
            if(affectedResults == 0)
            {
                throw new CarNotUpdatedException();
            }

            return car;
        }

        public class Command : IRequest<Car>
        {
            public int Id { get; set; }
            public string Make { get; set; } = null!;
            public string Model { get; set; } = null!;
            public string UniqueId { get; set; } = null!;
        }
    }
}
