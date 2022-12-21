using MediatR;

namespace Jorda.Application.CQRS.ToDoItem.Commands.DeleteToDoItem;

public class DeleteToDoItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public Guid SectionId { get; set; }
}

