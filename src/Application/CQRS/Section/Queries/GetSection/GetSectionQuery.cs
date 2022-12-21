using Jorda.Application.CQRS.Section.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.Section.Queries.GetSection;

public class GetSectionQuery : IRequest<SectionDTO>
{
    public Guid GoalId { get; set; }
    public Guid Id { get; set; }
}
