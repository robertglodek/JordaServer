using MediatR;


namespace Jorda.Server.Application.CQRS.ToDoItem.Commands.RealizeToDoItem;

public class RealizeToDoItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public Guid SectionId { get; set; }

}
