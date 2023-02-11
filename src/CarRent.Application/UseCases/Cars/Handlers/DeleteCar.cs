using AutoMapper;
using CarRent.Application.Exceptions;
using CarRent.Application.Exceptions.CarExceptions;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using CarRent.Domain;
using MediatR;

namespace CarRent.Application.UseCases.Cars.Handlers
{
    public class DeleteCar: IRequestHandler<DeleteCar.Command>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCar(ICarRepository carRepository, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(carRepository);
            ArgumentNullException.ThrowIfNull(mapper);

            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);

            var affectedResults = await _carRepository.Delete(car);
            if (affectedResults == 0)
            {
                throw new CarNotDeletedException();
            }

            return Unit.Value;
        }

        public class Command : IRequest
        {
            public int Id { get; set; }
        }
    }
}
