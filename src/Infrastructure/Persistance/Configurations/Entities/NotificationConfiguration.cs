using Jorda.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Infrastructure.Persistance.Configurations.Entities;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(n => n.Name)
           .HasMaxLength(30)
           .IsRequired();

        builder.Property(n => n.Description)
           .HasMaxLength(200);
           
    }
}

