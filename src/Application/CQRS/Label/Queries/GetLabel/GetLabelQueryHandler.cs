using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.CQRS.Label.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Label.Queries.GetLabel;

public class GetLabelQueryHandler : IRequestHandler<GetLabelQuery, LabelDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLabelQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LabelDTO> Handle(GetLabelQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Labels.FirstOrDefaultAsync(n => n.Id == request.Id);
        if (item == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Label), request.Id);
        }
        return _mapper.Map<LabelDTO>(item);
    }
}

