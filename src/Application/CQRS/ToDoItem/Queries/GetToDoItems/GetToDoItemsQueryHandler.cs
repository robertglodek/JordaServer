using AutoMapper;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.Common.Models;
using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;
using Jorda.Server.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jorda.Server.Application.CQRS.ToDoItem.Queries.GetToDoItems;

public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, PagedResult<ToDoItemDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetToDoItemsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<ToDoItemDTO>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _context.TodoItems.Include(n => n.Labels).Where(n => request.SearchPhrase == null
        || n.Name.ToLower().Contains(request.SearchPhrase.ToLower())
        || n.Description !=null && n.Description.ToLower().Contains(request.SearchPhrase.ToLower())
        || n.Labels.Any(n => n.Name.ToLower().Contains(request.SearchPhrase))
        || request.Active != null && n.Active == request.Active
        );

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelectors = new Dictionary<string, Expression<Func<Domain.Entities.ToDoItem, object?>>>
            {
               { nameof(Domain.Entities.ToDoItem.Created), n => n.Created },
               { nameof(Domain.Entities.ToDoItem.Name), n => n.Name },
               { nameof(Domain.Entities.ToDoItem.LastModified), n => n.LastModified},
               { nameof(Domain.Entities.ToDoItem.PriorityLevel), n => n.PriorityLevel},
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

        return new PagedResult<ToDoItemDTO>(_mapper.Map<List<ToDoItemDTO>>(entities),
            totalItemsCount, request.PageSize, request.PageNumber);
    }
}

