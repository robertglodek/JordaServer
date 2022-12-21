using AutoMapper;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Application.Common.Mappings;

public class GoalProfile:Profile
{
    public GoalProfile()
    {
        CreateMap<Goal, GoalDTO>()
            .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
            .ForMember(n => n.Name, s => s.MapFrom(y => y.Name))
            .ForMember(n => n.UserId, s => s.MapFrom(y => y.UserId))
            .ForMember(n => n.Description, s => s.MapFrom(y => y.Description))
            .ForMember(n => n.Colour, s => s.MapFrom(y => y.Colour.ToString()));
    }
}
