using AutoMapper;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.Common.Models;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using Jorda.Application.CQRS.Note.Queries.DTOs;
using Jorda.Server.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

namespace Jorda.Server.Application.CQRS.Note.Queries.GetNotes;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery,PagedResult<NoteDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetNotesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<NoteDTO>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _context.Notes.Include(n=>n.Labels).Where(n => request.SearchPhrase == null 
        || n.Name.ToLower().Contains(request.SearchPhrase.ToLower())
        || (n.Source!=null && n.Source.ToLower().Contains(request.SearchPhrase.ToLower()))
        || n.Description.ToLower().Contains(request.SearchPhrase.ToLower())
        || n.Labels.Any(n=>n.Name.ToLower().Contains(request.SearchPhrase))
        );


        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelectors = new Dictionary<string, Expression<Func<Domain.Entities.Note, object?>>>
                {
                   { nameof(Domain.Entities.Note.Created), n => n.Created },
                   { nameof(Domain.Entities.Note.Name), n => n.Name },
                   { nameof(Domain.Entities.Note.LastModified), n => n.LastModified},
                };
            var selectedColumn = columnsSelectors[request.SortBy];
            baseQuery = request.SortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
        var entities = await baseQuery
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync();

        var totalItemsCount = baseQuery.Count();

        
        return new PagedResult<NoteDTO>(_mapper.Map<List<NoteDTO>>(entities), 
            totalItemsCount, request.PageSize, request.PageNumber);
    }
}
