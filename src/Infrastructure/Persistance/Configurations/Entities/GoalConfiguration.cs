using Jorda.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jorda.Infrastructure.Persistance.Configurations.Entities;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(n => n.Description)
          .HasMaxLength(200)
          .IsRequired();
        builder
            .OwnsOne(b => b.Colour);
    }
}


