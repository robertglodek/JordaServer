using AutoMapper;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using Jorda.Application.CQRS.Goal.Queries.GetGoals;
using Jorda.Application.CQRS.Section.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jorda.Server.Application.CQRS.Section.Queries.GetSections
{

    public class GetSectionsQueryHandler : IRequestHandler<GetSectionsQuery, IEnumerable<SectionDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetSectionsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<SectionDTO>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Sections.Where(n => n.GoalId == request.GoalId).ToListAsync();
            return _mapper.Map<List<SectionDTO>>(items);
        }
    }

}
