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
            this.CreateMap<Test, TestDto>()
                .ForMember(dto => dto.Questions, options => options.MapFrom(t => t.Questions))
                .ForMember(dto => dto.Id, options => options.MapFrom(t => t.Id.ToString()))
                .MaxDepth(3);

            this.CreateMap<Question, QuestionDto>()
                .ForMember(dto => dto.Answers, options => options.MapFrom(q => q.Answers));

            this.CreateMap<TestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.TestViewModel>()
               .ForMember(vm => vm.CategoryName, options => options.MapFrom(x => x.Category.Name));

            this.CreateMap<TestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.TestViewModel>(MemberList.Source);


            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>()
               .ForMember(vm => vm.User, options => options.MapFrom(x => x.User.UserName))
               .ForMember(vm => vm.TestName, options => options.MapFrom(x => x.Test.TestName))
               .ForMember(vm => vm.Duration, options => options.MapFrom(x => x.Test.Duration))
               .ForMember(vm => vm.CategoryName, options => options.MapFrom(x => x.Test.Category.Name));

            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>(MemberList.Source);

            // Optional
            this.CreateMap<AnswerDto, TestSystem.Web.Models.TakeTestViewModels.AnswerViewModel>(MemberList.Source);
            this.CreateMap<QuestionDto, TestSystem.Web.Models.TakeTestViewModels.QuestionViewModel>(MemberList.Source);

            this.CreateMap<TestDto, TestListModel>()
                 .ForMember(x => x.Category, options => options.MapFrom(x => x.Category.Name));


            // Explicit Collection mappings:
            this.CreateMap<Test, TestDto>()
                .ForMember(t => t.Questions, o => o.MapFrom(x => x.Questions))
                .MaxDepth(3);

            this.CreateMap<Question, QuestionDto>()
                .ForMember(q => q.Answers, o => o.MapFrom(x => x.Answers))
                .MaxDepth(3);

            this.CreateMap<QuestionDto, TestSystem.Web.Models.TakeTestViewModels.QuestionViewModel>()
                .ForMember(q => q.Answers, o => o.MapFrom(x => x.Answers))
                .MaxDepth(3)
                .ReverseMap();

            this.CreateMap<Answer, AnswerDto>()
                .ForMember(dto => dto.AnsweredQuestions, o => o.MapFrom(x => x.AnsweredQuestions))
                .MaxDepth(3)
                .ReverseMap();

            this.CreateMap<UserTestDto, UserTest>(MemberList.Source)
                .ForMember(dto => dto.AnsweredQuestions, o => o.MapFrom(x => x.AnsweredQuestions))
                .MaxDepth(3);

            this.CreateMap<AnsweredQuestionDto, AnsweredQuestion>(MemberList.Source)
                .ForMember(db => db.Id, options => options.MapFrom(dto => dto.Id));

            this.CreateMap<TestSystem.Web.Models.TakeTestViewModels.IndexViewModel, UserTestDto>(MemberList.Source);
        }
    }
}
