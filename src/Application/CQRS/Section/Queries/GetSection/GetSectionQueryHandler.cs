using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Section.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Section.Queries.GetSection;

public class GetSectionQueryHandler : IRequestHandler<GetSectionQuery, SectionDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSectionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SectionDTO> Handle(GetSectionQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Sections.FirstOrDefaultAsync(n => n.Id == request.Id);
        if (item == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Section), request.Id);
        }
        return _mapper.Map<SectionDTO>(item);
    }
}
