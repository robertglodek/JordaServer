using AutoMapper;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.Common.Models;
using Jorda.Server.Application.CQRS.HistoryItem.Queries.DTOs;
using Jorda.Server.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jorda.Server.Application.CQRS.HistoryItem.Queries.GetHistoryItems;

public class GetHistoryItemsQueryHandler : IRequestHandler<GetHistoryItemsQuery, PagedResult<HistoryItemDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetHistoryItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<HistoryItemDTO>> Handle(GetHistoryItemsQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _context.HistoryItems.Where(n => request.SearchPhrase == null
        || n.Name.ToLower().Contains(request.SearchPhrase.ToLower())
        || (n.TaskStatus.ToString()== request.SearchPhrase));


        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelectors = new Dictionary<string, Expression<Func<Domain.Entities.HistoryItem, object?>>>
                {
                   { nameof(Domain.Entities.HistoryItem.Created), n => n.Created },
                   { nameof(Domain.Entities.HistoryItem.Name), n => n.Name }
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

        return new PagedResult<HistoryItemDTO>(_mapper.Map<List<HistoryItemDTO>>(entities),
            totalItemsCount, request.PageSize, request.PageNumber);
    }
}

