using Jorda.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Jorda.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Goal> Goals { get; }
    DbSet<HistoryItem> HistoryItems { get; }
    DbSet<Label> Labels { get; }
    DbSet<Note> Notes { get; }
    DbSet<Section> Sections { get; }
    DbSet<ToDoItem> TodoItems { get; }
    DbSet<UserFile> UserFiles { get; }
    DbSet<Notification> Notifications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
