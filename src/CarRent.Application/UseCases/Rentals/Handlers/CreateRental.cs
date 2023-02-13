using AutoMapper;
using CarRent.Application.Exceptions.RentalExceptions;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using MediatR;

namespace CarRent.Application.UseCases.Rentals.Handlers
{
    public class CreateRental : IRequestHandler<CreateRental.Command, Rental>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public CreateRental(IRentalRepository rentalRepository, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(rentalRepository);
            ArgumentNullException.ThrowIfNull(mapper);

            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }
        public async Task<Rental> Handle(Command request, CancellationToken cancellationToken)
        {
            var rental = _mapper.Map<Rental>(request);

            var affectedRecords = await _rentalRepository.Insert(rental);
            if(affectedRecords == 0)
            { 
                throw new RentalNotCreatedException();
            }

            rental = await _rentalRepository.GetByIdWithParentsAsync(rental.Id);

            return rental;
        }

        public class Command : IRequest<Rental>
        {
            public int UserId { get; set; }
            public int CarId { get; set; }
            public DateTimeOffset From { get; set; }
            public DateTimeOffset To { get; set; }
        }
    }
}
