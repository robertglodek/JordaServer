using Jorda.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jorda.Infrastructure.Persistance.Configurations.Entities;
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.Property(n => n.Name)
           .HasMaxLength(30)
           .IsRequired();

        builder.Property(n => n.Description)
           .HasMaxLength(200)
           .IsRequired();

        builder.Property(n => n.Source)
           .HasMaxLength(100);

    }
}

