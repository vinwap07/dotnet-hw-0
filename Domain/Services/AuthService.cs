using Domain.Dtos;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services;

public class AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
{
    public async Task<UserDto> LoginAsync(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var user  = await userRepository.GetByLoginAsync(command.Login, cancellationToken);
        if (user == null)
        {
            throw new ApplicationException("Invalid login or password");
        }
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new ApplicationException("Invalid login or password");
        }
        
        return user.ToDto();
    }
}