using Domain.Entities;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x.Entity is ITrackable && 
                        (x.State == EntityState.Added || x.State == EntityState.Modified));
    
        var utcNow = DateTimeOffset.UtcNow;

        foreach (var entry in entries)
        {
            var trackable = (ITrackable)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                trackable.CreatedAt = utcNow;
            }
            
            trackable.UpdatedAt = utcNow;
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }

}