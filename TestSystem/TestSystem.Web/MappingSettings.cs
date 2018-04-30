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
            this.CreateMap<Test, TestDto>(MemberList.Source)
                .ForMember(t => t.Questions, o => o.MapFrom(x => x.Questions))
                .ForMember(t => t.CategoryId, o => o.MapFrom(x => x.CategoryId.ToString()))
                
                .ForMember(t => t.Id, o => o.MapFrom(x => x.Id.ToString()))
                .MaxDepth(3);

            this.CreateMap<Question, QuestionDto>()
                .ForMember(dto => dto.Answers, options => options.MapFrom(q => q.Answers));

            //this.CreateMap<TestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.TestViewModel>()
            //   .ForMember(vm => vm.CategoryName, options => options.MapFrom(x => x.Category.Name));

            ViewMoDelToDtoMapping();
            DtoToViewModelMapping();
            DtoToDataModelMapping();
            DataModelToDtoMapping();





            //this.CreateMap<UserTest, TestResultDto>(MemberList.Source)
            //    .ForMember(db => db.UserName, option => option.MapFrom(x => x.User.UserName))
            //    .ForMember(db => db.TestName, option => option.MapFrom(x => x.Test.TestName))
            //    .ForMember(db => db.Category, option => option.MapFrom(x => x.Test.Category.Name))
            //    .ForMember(db => db.RequestedTime, option => option.MapFrom(x => x.Test.Duration));



            this.CreateMap<TestDto, AdministerTestViewModel>()
                .ForMember(x => x.Duration, option => option.MapFrom(o => o.Duration))
                .ForMember(t => t.TestName, o => o.MapFrom(x => x.TestName))
                .ForMember(t => t.Category, o => o.MapFrom(x => x.Category.Name))
                .ForMember(t => t.Questions, o => o.MapFrom(x => x.Questions))
                .MaxDepth(3)
                .ReverseMap();

            //this.CreateMap<UserTestDto, TestSystem.Web.Areas.Administration.Models.DashboardViewModels.ResultViewModel>()
            //   .ForMember(vm => vm.User, options => options.MapFrom(x => x.User.UserName))
            //   .ForMember(vm => vm.TestName, options => options.MapFrom(x => x.Test.TestName))
            //   .ForMember(vm => vm.Duration, options => options.MapFrom(x => x.Test.Duration))
            //   .ForMember(vm => vm.CategoryName, options => options.MapFrom(x => x.Test.Category.Name));


            // Optional
            this.CreateMap<AnswerDto, TestSystem.Web.Models.TakeTestViewModels.AnswerViewModel>(MemberList.Source);


            this.CreateMap<QuestionDto, TestSystem.Web.Models.TakeTestViewModels.QuestionViewModel>(MemberList.Source);

            this.CreateMap<QuestionViewModel, QuestionDto>(MemberList.Source);

            this.CreateMap<AnswerViewModel, AnswerDto>(MemberList.Source);



            this.CreateMap<Question, QuestionDto>(MemberList.Source)
 .ForMember(q => q.Answers, o => o.MapFrom(x => x.Answers))
 .MaxDepth(3);

            // Explicit Collection mappings:
            this.CreateMap<Category, CategoryDto>()
    .ForMember(dto => dto.Tests, db => db.MapFrom(x => x.Tests))
    .ForMember(dto => dto.Id, db => db.MapFrom(x => x.Id.ToString()))
    .MaxDepth(5);



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
                .ForMember(t => t.TestName, o => o.MapFrom(t => t.TestName));

            this.CreateMap<UserTest, TestResultDto>()
                .ForMember(dto => dto.UserId, o => o.MapFrom(dm => dm.UserId))
                .ForMember(dto => dto.TestId, o => o.MapFrom(dm => dm.TestId))
                .ForMember(dto => dto.UserName, o => o.MapFrom(dm => dm.User.UserName))
                .ForMember(dto => dto.TestName, o => o.MapFrom(dm => dm.Test.TestName))
                .ForMember(dto => dto.CategoryName, o => o.MapFrom(dm => dm.Test.Category.Name))
                .ForMember(dto => dto.RequestedTime, o => o.MapFrom(dm => dm.Test.Duration))
                .ForMember(dto => dto.ExecutionTime, o => o.MapFrom(dm => dm.SubmittedOn.Value.Subtract(dm.StartTime)))
                .ForMember(dto => dto.Result, o => o.MapFrom(dm => dm.Score));


            //this.CreateMap<UserTest, TestResultDto>(MemberList.Destination)
            //   .ForMember(ut => ut.UserName, o => o.MapFrom(ut => ut.User.UserName))
            //   .ForMember(ut => ut.TestName, o => o.MapFrom(ut => ut.Test.TestName))
            //   .ForMember(ut => ut.Category, o => o.MapFrom(ut => ut.Test.Category.Name))
            //   .ForMember(ut => ut.ExecutionTime, o => o.MapFrom(ut => ut.SubmittedOn.Value.Subtract(ut.StartTime.Value)))
            //   .ForMember(ut => ut.Result, o => o.MapFrom(ut => ut.Score));
        }
    }
}
