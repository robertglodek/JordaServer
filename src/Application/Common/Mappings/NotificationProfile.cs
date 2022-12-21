using AutoMapper;
using Jorda.Server.Application.CQRS.Notification.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Server.Application.Common.Mappings;

public class NotificationProfile:Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, NotificationDTO>()
            .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
            .ForMember(n => n.Name, s => s.MapFrom(y => y.Name))
            .ForMember(n => n.Active, s => s.MapFrom(y => y.Active))
            .ForMember(n => n.Description, s => s.MapFrom(y => y.Description)); 
    }
}
