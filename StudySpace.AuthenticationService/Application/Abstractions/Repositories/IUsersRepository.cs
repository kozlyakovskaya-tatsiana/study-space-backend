using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IUsersRepository : IRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}

