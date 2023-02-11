using CarRent.Database.Interfaces;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FitnessApp.Database.Base;
using Microsoft.Extensions.Logging;

namespace CarRent.Database.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseContext context, ILogger<BaseRepository<User>> logger) : base(context, logger)
        {
        }
    }
}
