using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Note.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Note.Queries.GetNote
{
    public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NoteDTO> Handle(GetNoteQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Notes.FirstOrDefaultAsync(n => n.Id == request.Id && n.SectionId == request.SectionId);
            if (item == null)
            {
                throw new NotFoundException(nameof(Server.Domain.Entities.Note), request.Id);
            }
            return _mapper.Map<NoteDTO>(item);
        }
    }
}
