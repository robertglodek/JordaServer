using Jorda.Application.Common.Interfaces;
using MediatR;


namespace Jorda.Server.Application.CQRS.Label.Commands.CreateLabel
{
    public class CreateLabelCommandHandler : IRequestHandler<CreateLabelCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateLabelCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateLabelCommand request, CancellationToken cancellationToken)
        {
            var entity = new Server.Domain.Entities.Label
            {
                Name = request.Name,
                UserId = Guid.Parse(_currentUserService.UserId!)   
            };
            _context.Labels.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
