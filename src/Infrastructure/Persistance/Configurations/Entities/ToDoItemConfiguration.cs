using Jorda.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jorda.Infrastructure.Persistance.Configurations.Entities;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.Property(n => n.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(n => n.Description)
            .HasMaxLength(200);

       
    }
}
