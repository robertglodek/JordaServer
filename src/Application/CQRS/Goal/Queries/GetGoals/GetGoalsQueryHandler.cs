using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.Goal.Queries.GetGoals;

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery,IEnumerable<GoalDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetGoalsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<GoalDTO>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Goals.Where(n => n.UserId == _currentUserService.UserId!).ToListAsync();
        return _mapper.Map<List<GoalDTO>>(items) ;
    }
}
