using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        
        return services;
    }
}