using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Constants.UserLimitations;
using Jorda.Server.Application.Common.Exceptions;
using Jorda.Server.Application.Common.Resources.Limitation;
using Jorda.Server.Domain.Entities;
using Jorda.Server.Domain.Events.ToDoItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.ToDoItem.Commands.CreateToDoItem;

public class CreateToDoitemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateToDoitemCommandHandler(IApplicationDbContext context,ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {

        var section = _context.Sections.FirstOrDefaultAsync(n => n.Id == request.SectionId);
        if (section == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Section), request.SectionId);
        }

        var todoItemsCount = _context.TodoItems.Count(n => n.SectionId == request.SectionId);

        var maxItemsCount = UserConstraintsConstants.GetMaxActiveToDoItemsPerSectionCount(_currentUserService.UserRole!);

        if (todoItemsCount > maxItemsCount)
        {
            throw new LimitationException($"TodoItems count exceeded: {maxItemsCount}",
                string.Format(Limitation.ToDoItemsCountExceeded, maxItemsCount));
        }

        var entity = new Server.Domain.Entities.ToDoItem
        {
            Description = request.Description,
            Name = request.Name,
            SectionId = request.SectionId,
            Active = false,
            DueDate = request.DueDate,
            Interval = request.Interval
        };
        entity.AddDomainEvent(new ToDoItemCreatedEvent(entity));
        await _context.TodoItems.AddAsync(entity);
        await _context.HistoryItems.AddAsync(new HistoryItem()
        {
            UserId = _currentUserService.UserId!,
            Name = entity.Name,
            TaskStatus = Server.Domain.Enums.TaskStatus.ToDoItemAdded
        });
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
