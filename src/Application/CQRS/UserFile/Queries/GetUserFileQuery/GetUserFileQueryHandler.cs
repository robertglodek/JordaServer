using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.UserFile.Queries.GetUserFileQuery;

public class GetUserFileQueryHandler : IRequestHandler<GetUserFileQuery, UserFileDetailsDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserFileQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserFileDetailsDTO> Handle(GetUserFileQuery request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FirstOrDefaultAsync(n => n.Id == request.GoalId);
        if(goal == null) 
        {
            throw new NotFoundException(nameof(Domain.Entities.Goal), request.GoalId);
        }

        var item = await _context.UserFiles.FirstOrDefaultAsync(n => n.Id == request.Id);
        if (item == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.UserFile), request.Id);
        }
        return _mapper.Map<UserFileDetailsDTO>(item);
    }
}
