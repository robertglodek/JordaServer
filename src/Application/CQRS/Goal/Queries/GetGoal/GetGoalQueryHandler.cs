
using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.Goal.Queries.GetGoal;

public class GetGoalQueryHandler : IRequestHandler<GetGoalQuery,GoalDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetGoalQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GoalDTO> Handle(GetGoalQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Goals.FirstOrDefaultAsync(n => n.Id == request.Id);
        if (item == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Goal), request.Id);
        }
        return _mapper.Map<GoalDTO>(item);
    }
}
