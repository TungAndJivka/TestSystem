using AutoMapper;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Web.Areas.Administration.Models.Shared;

namespace TestSystem.Web
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            
            this.CreateMap<TestDto, TestListModel>()
                 .ForMember(x => x.Category, options => options.MapFrom(x => x.Category.Name));


            this.CreateMap<UserTest, ResultListDto>(MemberList.Source)
                .ForMember(db => db.UserName, option => option.MapFrom(x => x.User.UserName))
                .ForMember(db => db.TestName, option => option.MapFrom(x => x.Test.TestName))
                .ForMember(db => db.Category, option => option.MapFrom(x => x.Test.Category.Name))
                .ForMember(db => db.RequestedTime, option => option.MapFrom(x => x.Test.Duration));
                
            //this.CreateMap<PostViewModel, PostDto>(MemberList.Source);
            //this.CreateMap<PostDto, Post>(MemberList.Source);
            //this.CreateMap<CommentDto, Comment>(MemberList.Source);

        }
    }
}
