using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Entities;
using Jorda.Infrastructure.Common;
using Jorda.Infrastructure.Persistance.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Jorda.Server.Infrastructure.Identity.Models;

namespace Jorda.Infrastructure.Persistance;
public class ApplicationDbContext:IdentityDbContext<JordaUser, JordaRole, string>, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly IMediator _mediator;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext, 
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,IMediator mediator) :base(dbContext)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _mediator = mediator;
    }
    public DbSet<ToDoItem> TodoItems => Set<ToDoItem>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Notification> Informations => Set<Notification>();
    public DbSet<UserFile> UserFiles => Set<UserFile>();
    public DbSet<HistoryItem> HistoryItems => Set<HistoryItem>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<Notification> Notifications => Set<Notification>();

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
