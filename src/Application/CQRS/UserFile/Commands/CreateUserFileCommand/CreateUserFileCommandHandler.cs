using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Constants.UserLimitations;
using Jorda.Server.Application.Common.Exceptions;
using Jorda.Server.Application.Common.Extensions;
using Jorda.Server.Application.Common.Resources.Limitation;
using Jorda.Server.Domain.Events.UserFile;
using MediatR;

namespace Jorda.Server.Application.CQRS.UserFile.Commands.CreateUserFileCommand;

public class CreateUserFileCommandHandler : IRequestHandler<CreateUserFileCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateUserFileCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateUserFileCommand request, CancellationToken cancellationToken)
    {

        var filesCount = _context.UserFiles.Count(n => n.GoalId == request.GoalId);

        var maxFilesCount = UserConstraintsConstants.GetUserFilesPerGoalCount(_currentUserService.UserRole!);

        if (filesCount > maxFilesCount)
        {
            throw new LimitationException($"UserFiles count exceeded: {maxFilesCount}",
                string.Format(Limitation.UserFilesCountExceeded, maxFilesCount));
        }
       
        var entity = new Domain.Entities.UserFile
        {
            Name = request.File.FileName,
            GoalId = request.GoalId,
            ContentType = request.File.ContentType,
            Data = Convert.ToBase64String(await request.File.GetBytesAsync())
        };

        entity.AddDomainEvent(new UserFileCreatedEvent(entity));
        _context.UserFiles.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
