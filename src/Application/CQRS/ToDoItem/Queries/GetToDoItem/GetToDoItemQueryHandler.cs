using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.ToDoItem.Queries.GetToDoItem;

public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, ToDoItemDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetToDoItemQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ToDoItemDTO> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.TodoItems.FirstOrDefaultAsync(n => n.Id == request.Id);
        if (item == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.ToDoItem), request.Id);
        }
        return _mapper.Map<ToDoItemDTO>(item);
    }
}
