using AutoMapper;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.UserFile.Queries.GetUserFilesQuery;

public class GetUserFilesQueryHandler : IRequestHandler<GetUserFilesQuery, IEnumerable<UserFileDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetUserFilesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<UserFileDTO>> Handle(GetUserFilesQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.UserFiles.Where(n => n.GoalId == request.GoalId)
            .Select(n=>new Domain.Entities.UserFile() { Id=n.Id, Name=n.Name }).ToListAsync();
   
        return _mapper.Map<IEnumerable<UserFileDTO>>(items);
    }
}
