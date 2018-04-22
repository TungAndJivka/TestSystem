using AutoMapper;
using TestSystem.DTO;
using TestSystem.Web.Areas.Administration.Models.Shared;

namespace TestSystem.Web
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            //this.CreateMap<PostDto, PostViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email))
            //       .ReverseMap();

            //this.CreateMap<CommentDto, CommentViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email))
            //       .ReverseMap();

            this.CreateMap<TestDto, TestListModel>()
                 .ForMember(x => x.Category, options => options.MapFrom(x => x.Category.Name));

            //this.CreateMap<PostViewModel, PostDto>(MemberList.Source);
            //this.CreateMap<PostDto, Post>(MemberList.Source);
            //this.CreateMap<CommentDto, Comment>(MemberList.Source);

        }
    }
}
