using CarRent.Database.Interfaces;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FitnessApp.Database.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRent.Database.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseContext context, ILogger<BaseRepository<User>> logger) : base(context, logger)
        {
        }

        public async Task<bool> DoesUserExistAsync(int userId, CancellationToken cancellationToken)
        {
            return await Select()
                .Where(x => x.Id== userId)
                .AnyAsync(cancellationToken);
        }
    }
}
