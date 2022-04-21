using AutoMapper;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;

namespace ForumBackend.Mapping.Profiles;

public class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        CreateMap<ForumUserDto, ForumUser>()
            .ForMember(u => u.Nickname, c => 
                c.MapFrom(d => d.Nickname))
            .ForMember(u => u.Email, c => 
                c.MapFrom(d => d.Email))
            .ReverseMap()
            .ForMember(d => d.Password, 
                c =>
                c.MapFrom<string?>(_ => null));
    }
}