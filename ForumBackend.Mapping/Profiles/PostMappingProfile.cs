using AutoMapper;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;

namespace ForumBackend.Mapping.Profiles;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<ForumPostDto, ForumPost>()
            .ForMember(p => p.Description, c =>
                c.MapFrom(p => p.Description))
            .ForMember(p => p.Text, c =>
                c.MapFrom(p => p.Text))
            .ReverseMap()
            .ForMember(p => p.CreatedAt, c =>
                c.MapFrom(p => DateTime.UnixEpoch.AddSeconds(p.CreatedAt)))
            .ForMember(p => p.Id, c =>
                c.MapFrom(p => p.Id))
            .ForMember(p => p.Author, c =>
                c.MapFrom(p => p.Author.Nickname))
            .ForMember(p => p.Likes, c =>
                c.MapFrom(p => 0));
        // TODO: Count likes
    }
}