using AutoMapper;
using Jorda.Application.CQRS.Section.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Application.Common.Mappings;

public class SectionProfile:Profile
{
    public SectionProfile()
    {
        CreateMap<Section, SectionDTO>()
           .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
           .ForMember(n => n.Name, s => s.MapFrom(y => y.Name))
           .ForMember(n => n.GoalId, s => s.MapFrom(y => y.GoalId))
           .ForMember(n => n.Description, s => s.MapFrom(y => y.Description));   
    }
}
