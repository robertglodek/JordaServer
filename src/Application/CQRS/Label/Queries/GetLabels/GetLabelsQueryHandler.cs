using AutoMapper;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using Jorda.Application.CQRS.Goal.Queries.GetGoals;
using Jorda.Server.Application.CQRS.Label.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jorda.Server.Application.CQRS.Label.Queries.GetLabels
{
    public class GetLabelsQueryHandler : IRequestHandler<GetLabelsQuery, IEnumerable<LabelDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetLabelsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<LabelDTO>> Handle(GetLabelsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Labels.Where(n => n.UserId == Guid.Parse(_currentUserService.UserId!)).ToListAsync();
            return _mapper.Map<List<LabelDTO>>(items);
        }
    }

}
