﻿using CarRent.Database.Interfaces.Base;
using CarRent.Domain;

namespace CarRent.Database.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> DoesUserExistAsync(int userId, CancellationToken cancellationToken);
    }
}
