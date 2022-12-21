using AutoMapper;
using Jorda.Server.Application.CQRS.HistoryItem.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Server.Application.Common.Mappings;

public class HistoryItemProfile : Profile
{
    public HistoryItemProfile()
    {
        CreateMap<HistoryItem, HistoryItemDTO>()
            .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
            .ForMember(n => n.Name, s => s.MapFrom(y => y.Name))
            .ForMember(n => n.UserId, s => s.MapFrom(y => y.UserId))
            .ForMember(n => n.TaskStatus, s => s.MapFrom(y => y.TaskStatus))
            .ForMember(n => n.Created, s => s.MapFrom(y => y.Created));
    }
}
