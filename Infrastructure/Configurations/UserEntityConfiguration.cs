using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Login)
            .IsRequired()
            .HasMaxLength(128);
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);
    }
}