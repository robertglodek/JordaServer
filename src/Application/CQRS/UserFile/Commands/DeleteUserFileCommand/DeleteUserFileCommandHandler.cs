using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Events.UserFile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.UserFile.Commands.DeleteUserFileCommand;

public class DeleteUserFileCommandHandler : IRequestHandler<DeleteUserFileCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserFileCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserFileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserFiles
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.UserFile), request.Id);
        }
        entity.AddDomainEvent(new UserFileDeletedEvent(entity));
        _context.UserFiles.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
