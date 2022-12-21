using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.ToDoItem.Commands.UpdateToDoItem;

public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateToDoItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Jorda.Server.Domain.Entities.ToDoItem), request.Id);
        }
        if (request.SectionId != entity.SectionId)
        {
            var section = _context.Sections.FirstOrDefaultAsync(n => n.Id == request.SectionId);
            if (section == null)
            {
                throw new NotFoundException(nameof(Jorda.Server.Domain.Entities.Section), request.SectionId);
            }
        }
        entity.Description = request.Description;
        entity.Name = request.Name;
        entity.SectionId = request.SectionId;
        entity.DueDate = request.DueDateUtc;
        entity.Interval = request.Interval;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
