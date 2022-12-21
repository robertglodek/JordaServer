using AutoMapper;
using Jorda.Server.Application.Common.Models.Identity.Responses;
using Jorda.Server.Infrastructure.Identity.Models;

namespace Jorda.Server.Infrastructure.Common.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<JordaUser, UserResponse>()
            .ForMember(n => n.Id, s => s.MapFrom(y => y.Id))
            .ForMember(n => n.Email, s => s.MapFrom(y => y.Email));
    }
}
