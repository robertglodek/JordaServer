using AutoMapper;
using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Application.Common.Mappings;

public class ToDoItemProfile:Profile
{
    public ToDoItemProfile()
    {

        CreateMap<ToDoItem, ToDoItemDTO>()
           .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
           .ForMember(n => n.Name, s => s.MapFrom(y => y.Name))
           .ForMember(n => n.Interval, s => s.MapFrom(y => y.Interval))
           .ForMember(n => n.Description, s => s.MapFrom(y => y.Description))
           .ForMember(n => n.DueDate, s => s.MapFrom(y => y.DueDate))
           .ForMember(n => n.SectionId, s => s.MapFrom(y => y.SectionId))
           .ForMember(n => n.Done, s => s.MapFrom(y => y.Active))
           .ForMember(n => n.PriorityLevel, s => s.MapFrom(y => y.PriorityLevel));
    }
}
