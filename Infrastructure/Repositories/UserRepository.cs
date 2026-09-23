using Domain.Dtos;
using Domain.Entities;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<User> _users = context.Set<User>();

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _users.Where(u => u.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _users.AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<User>> GetByFiltersAsync(UserFilters filters, CancellationToken cancellationToken = default)
    {
        var query = _users.AsNoTracking();

        if (filters.CreatedAtFrom.HasValue)
        {
            query = query.Where(u => u.CreatedAt >= filters.CreatedAtFrom);
        }

        if (filters.CreatedAtTo.HasValue)
        {
            query = query.Where(u => u.CreatedAt <= filters.CreatedAtTo);
        }

        if (filters.UpdatedAtFrom.HasValue)
        {
            query = query.Where(u => u.UpdatedAt >= filters.UpdatedAtFrom);
        }

        if (filters.UpdatedAtTo.HasValue)
        {
            query = query.Where(u => u.UpdatedAt <= filters.UpdatedAtTo);
        }
        
        return await query.ToListAsync(cancellationToken);
    }

    public Task<User?> GetByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return _users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);
    }
}