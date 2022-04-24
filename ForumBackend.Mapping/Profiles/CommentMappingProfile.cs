using AutoMapper;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;

namespace ForumBackend.Mapping.Profiles;

public class CommentMappingProfile: Profile
{
    public CommentMappingProfile()
    {
        CreateMap<ForumCommentDto, ForumComment>()
            .ForMember(comm => comm.Text, c =>
                c.MapFrom(comm => comm.Text))
            .ReverseMap()
            .ForMember(comm => comm.Id, c =>
                c.MapFrom(comm => comm.Id))
            .ForMember(comm => comm.Author, c =>
                c.MapFrom(comm => comm.Author.Nickname))
            .ForMember(comm => comm.CreatedAt, c =>
                c.MapFrom(comm => DateTime.UnixEpoch.AddSeconds(comm.CreatedAt)))
            .ForMember(comm => comm.Likes, c =>
                c.MapFrom(comm => comm.LikedUsers.Count));
    }
}