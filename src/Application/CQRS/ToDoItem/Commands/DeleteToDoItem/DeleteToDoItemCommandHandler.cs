using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Events.ToDoItem;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Jorda.Application.CQRS.ToDoItem.Commands.DeleteToDoItem;

public class DeleteToDoItemCommandhandler : IRequestHandler<DeleteToDoItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteToDoItemCommandhandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Jorda.Server.Domain.Entities.ToDoItem), request.Id);
        }
        entity.AddDomainEvent(new ToDoItemDeletedEvent(entity));
        _context.TodoItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
