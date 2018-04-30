using AutoMapper;
using System;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Web.Areas.Administration.Models.CreateTestViewModels;
using TestSystem.Web.Areas.Administration.Models.DashboardViewModels;

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

            ViewMoDelToDtoMapping();
            DtoToViewModelMapping();
            DtoToDataModelMapping();
            DataModelToDtoMapping();
                
            
           


            this.CreateMap<UserTest, TestResultDto>(MemberList.Source)
                .ForMember(db => db.UserName, option => option.MapFrom(x => x.User.UserName))
                .ForMember(db => db.TestName, option => option.MapFrom(x => x.Test.TestName))
                .ForMember(db => db.Category, option => option.MapFrom(x => x.Test.Category.Name))
                .ForMember(db => db.RequestedTime, option => option.MapFrom(x => x.Test.Duration));



            this.CreateMap<TestDto, AdministerTestViewModel>()
                .ForMember(x => x.Duration, option => option.MapFrom(o => o.Duration))
                .ForMember(t => t.TestName, o => o.MapFrom(x => x.TestName))
                .ForMember(t => t.Category, o => o.MapFrom(x => x.Category.Name))
                .ForMember(t => t.Questions, o => o.MapFrom(x => x.Questions))
                .MaxDepth(3)
                .ReverseMap();

            this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>()
               .ForMember(vm => vm.User, options => options.MapFrom(x => x.User.UserName))
               .ForMember(vm => vm.TestName, options => options.MapFrom(x => x.Test.TestName))
               .ForMember(vm => vm.Duration, options => options.MapFrom(x => x.Test.Duration))
               .ForMember(vm => vm.CategoryName, options => options.MapFrom(x => x.Test.Category.Name));


            // Optional
            this.CreateMap<AnswerDto, TestSystem.Web.Models.TakeTestViewModels.AnswerViewModel>(MemberList.Source);
            this.CreateMap<QuestionDto, TestSystem.Web.Models.TakeTestViewModels.QuestionViewModel>(MemberList.Source);

            this.CreateMap<QuestionViewModel, QuestionDto>(MemberList.Source);

            this.CreateMap<AnswerViewModel, AnswerDto>(MemberList.Source);

            this.CreateMap<Test, TestDto>(MemberList.Source)
             .ForMember(q => q.Questions, o => o.MapFrom(x => x.Questions))
             .MaxDepth(3)
             .ReverseMap();

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

       

        private void ViewMoDelToDtoMapping()
        {
            this.CreateMap<AdministerTestViewModel, AdministerTestDto>()
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions))
                .ReverseMap();

            this.CreateMap<QuestionViewModel, AdministerQuestionDto>()
               .ForMember(q => q.Answers, o => o.MapFrom(q => q.Answers))
               .ReverseMap();

            this.CreateMap<AnswerViewModel, AdministerAnswerDto>()
                .ForMember(a => a.Content, o => o.MapFrom(a => a.Content))
                .ForMember(a => a.IsCorrect, o => o.MapFrom(a => a.IsCorrect))
                .ReverseMap();
        }

        private void DtoToViewModelMapping()
        {
            this.CreateMap<ExistingTestDto, ExistingTestViewModel>(MemberList.Destination);
            this.CreateMap<TestResultDto, TestResultViewModel>(MemberList.Destination);
        }

        private void DtoToDataModelMapping()
        {
            this.CreateMap<AdministerTestDto, Test>()
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions));

            this.CreateMap<AdministerQuestionDto, Question>();


            this.CreateMap<AdministerAnswerDto, Answer>();

        }


        private void DataModelToDtoMapping()
        {
            this.CreateMap<Test, ExistingTestDto>()
                .ForMember(t => t.Category, o => o.MapFrom(t => t.Category.Name))
                .ForMember(t => t.TestName, o => o.MapFrom(t => t.Category.Name));

            this.CreateMap<UserTest, TestResultDto>(MemberList.Destination)
               .ForMember(ut => ut.UserName, o => o.MapFrom(ut => ut.User.UserName))
               .ForMember(ut => ut.TestName, o => o.MapFrom(ut => ut.Test.TestName))
               .ForMember(ut => ut.Category, o => o.MapFrom(ut => ut.Test.Category.Name))
               .ForMember(ut => ut.ExecutionTime, o => o.MapFrom(ut => ut.SubmittedOn.Value.Subtract(ut.StartTime.Value)))
               .ForMember(ut => ut.Result, o => o.MapFrom(ut => ut.Score));
        }
    }
}
