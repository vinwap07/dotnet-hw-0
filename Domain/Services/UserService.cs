using Domain.Abstractions;
using Domain.Mappers;
using Domain.Abstractions.Repositories;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services;

public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
{
    public async Task<UserDto> AddUser(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var isExists = (await userRepository.GetByLoginAsync(request.Login, cancellationToken)) != null;
        if (!isExists)
        {
            throw new Exception($"User with login {request.Login} already exists.");
        }
        
        var user = new User()
        {
            Login = request.Login
        };
        
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);
        
        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user.ToDto(); 
    }

    public async Task<UserDto> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            throw new Exception($"User does not exist.");
        }
        
        user.Login = request.Login ?? user.Login;
        user.PasswordHash = request.NewPassword != null ? passwordHasher.HashPassword(user, request.NewPassword) : user.PasswordHash;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.ToDto();
    }

    public async Task DeleteUser(Guid id, CancellationToken cancellationToken = default)
    {
        await userRepository.DeleteAsync(id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<UserDto>> GetUsers(CancellationToken cancellationToken = default)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);
        return users.Select(u => u.ToDto()).ToList();
    }

    public async Task<List<UserDto>> GetUsers(UserFilters filters, CancellationToken cancellationToken = default)
    {
        var users = await userRepository.GetByFiltersAsync(filters, cancellationToken);
        return users.Select(u => u.ToDto()).ToList();
    }
}