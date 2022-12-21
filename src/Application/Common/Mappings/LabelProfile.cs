using AutoMapper;
using Jorda.Server.Application.CQRS.Label.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Server.Application.Common.Mappings;

public class LabelProfile : Profile
{
    public LabelProfile()
    {
        CreateMap<Label, LabelDTO>()
            .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
            .ForMember(n => n.Name, s => s.MapFrom(y => y.Name));   
    }
}
