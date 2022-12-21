using AutoMapper;
using Jorda.Application.CQRS.Note.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Application.Common.Mappings;
public class NoteProfile:Profile
{
    public NoteProfile()
    {
        CreateMap<Note, NoteDTO>()
            .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
            .ForMember(n => n.Name, s => s.MapFrom(y => y.Name))
            .ForMember(n => n.Source, s => s.MapFrom(y => y.Source))
            .ForMember(n => n.Description, s => s.MapFrom(y => y.Description))
            .ForMember(n => n.SectionId, s => s.MapFrom(y => y.SectionId));
    }
}
