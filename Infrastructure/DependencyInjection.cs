using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<IUnitOfWork>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}