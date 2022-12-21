using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Label.Commands.DeleteLabel;

public class DeleteLabelCommandHandler : IRequestHandler<DeleteLabelCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteLabelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteLabelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Labels
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Label), request.Id);
        }
        _context.Labels.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
