﻿using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using MediatR;

namespace CarRent.Application.UseCases.Rentals.Handlers
{
    public class GetUpcomingRentals : IRequestHandler<GetUpcomingRentals.Query, IEnumerable<Rental>>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetUpcomingRentals(IRentalRepository rentalRepository)
        {
            ArgumentNullException.ThrowIfNull(rentalRepository);

            _rentalRepository = rentalRepository;
        }
        public async Task<IEnumerable<Rental>> Handle(Query request, CancellationToken cancellationToken)
        {
            var from = request.DateTimeFrom ?? DateTimeOffset.Now;
            return await _rentalRepository.GetRentalsByUserIdAsync(from, request.UserId);
        }

        public class Query: IRequest<IEnumerable<Rental>>
        {
            public int UserId { get; set; }
            public DateTimeOffset? DateTimeFrom { get; set; }
        }
    }
}
