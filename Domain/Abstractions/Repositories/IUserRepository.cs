using Domain.Dtos;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository
{
    public Task AddAsync(User user, CancellationToken cancellationToken = default);
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<List<User>> GetByFiltersAsync(UserFilters filters, CancellationToken cancellationToken = default);
    public Task<User?> GetByLoginAsync(string login, CancellationToken cancellationToken = default);
}