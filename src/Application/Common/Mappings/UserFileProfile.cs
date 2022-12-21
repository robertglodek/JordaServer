using AutoMapper;
using Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;
using Jorda.Server.Domain.Entities;

namespace Jorda.Server.Application.Common.Mappings;

public class UserFileProfile:Profile
{
    public UserFileProfile()
    {
        CreateMap<UserFile, UserFileDTO>()
           .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
           .ForMember(n => n.Name, s => s.MapFrom(y => y.Name));

        CreateMap<UserFile, UserFileDetailsDTO>()
           .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
           .ForMember(n => n.Data, s => s.MapFrom(y => y.Data))
           .ForMember(n => n.ContentType, s => s.MapFrom(y => y.ContentType))
           .ForMember(n => n.Name, s => s.MapFrom(y => y.Name));
    }
}
