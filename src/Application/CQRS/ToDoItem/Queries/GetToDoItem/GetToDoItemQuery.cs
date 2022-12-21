using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.ToDoItem.Queries.GetToDoItem;

public class GetToDoItemQuery:IRequest<ToDoItemDTO>
{

    public Guid Id { get; set; }

}
