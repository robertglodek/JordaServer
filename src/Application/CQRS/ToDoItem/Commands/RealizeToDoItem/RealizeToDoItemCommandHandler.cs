using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.ToDoItem.Commands.RealizeToDoItem
{

    public class RealizeToDoItemCommandHandler : IRequestHandler<RealizeToDoItemCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public RealizeToDoItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(RealizeToDoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.ToDoItem), request.Id);
            }
            if (request.SectionId != entity.SectionId)
            {
                var section = _context.Sections.FirstOrDefaultAsync(n => n.Id == request.SectionId);
                if (section == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Section), request.SectionId);
                }
            }
            entity.Succeeded();
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
