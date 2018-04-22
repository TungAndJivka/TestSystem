using AutoMapper;
using TestSystem.DTO;

namespace TestSystem.Web
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            this.CreateMap<TestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.TestViewModel>()
               .ForMember(vm => vm.CategoryName, options => options.MapFrom(x => x.Category.Name));

            this.CreateMap<TestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.TestViewModel>(MemberList.Source);



            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>()
               .ForMember(vm => vm.User, options => options.MapFrom(x => x.User.UserName));

            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>()
               .ForMember(vm => vm.TestName, options => options.MapFrom(x => x.Test.TestName));

            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>()
               .ForMember(vm => vm.Duration, options => options.MapFrom(x => x.Test.Duration));

            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>()
               .ForMember(vm => vm.CategoryName, options => options.MapFrom(x => x.Test.Category.Name));

            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>(MemberList.Source);


            //this.CreateMap<PostViewModel, PostDto>(MemberList.Source);
            //this.CreateMap<PostDto, Post>(MemberList.Source);
            //this.CreateMap<CommentDto, Comment>(MemberList.Source);

        }
    }
}
