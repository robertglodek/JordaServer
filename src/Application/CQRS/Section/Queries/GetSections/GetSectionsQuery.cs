using Jorda.Application.CQRS.Section.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.Section.Queries.GetSections
{

    public class GetSectionsQuery : IRequest<IEnumerable<SectionDTO>>
    {
        public Guid GoalId { get; set; }
    }
}
