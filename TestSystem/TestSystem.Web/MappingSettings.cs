using AutoMapper;
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
            ViewModelToDtoMapping();

            DtoToViewModelMapping();

            DtoToDataModelMapping();

            DataModelToDtoMapping();
        }


        private void ViewModelToDtoMapping()
        {
            this.CreateMap<QuestionViewModel, QuestionDto>(MemberList.Source);

            this.CreateMap<AnswerViewModel, AnswerDto>(MemberList.Source);

            this.CreateMap<AdministerTestViewModel, AdministerTestDto>()
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions))
                .ForMember(t => t.Category, o => o.MapFrom(t => t.Category))
                .ReverseMap();

            this.CreateMap<QuestionViewModel, AdministerQuestionDto>()
               .ForMember(q => q.Answers, o => o.MapFrom(q => q.Answers))
               .ReverseMap();

            this.CreateMap<AnswerViewModel, AdministerAnswerDto>()
                .ForMember(a => a.Content, o => o.MapFrom(a => a.Content))
                .ForMember(a => a.IsCorrect, o => o.MapFrom(a => a.IsCorrect))
                .ReverseMap();

            this.CreateMap<TestSystem.Web.Models.TakeTestViewModels.IndexViewModel, UserTestDto>(MemberList.Source);

            this.CreateMap<AdministerTestViewModel, EditTestDto>()
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions))
                .ForMember(t => t.Category, o => o.MapFrom(t => t.Category))
                .ReverseMap();

            this.CreateMap<QuestionViewModel, EditQuestionDto>()
               .ForMember(q => q.Answers, o => o.MapFrom(q => q.Answers))
               .ReverseMap();

            this.CreateMap<AnswerViewModel, EditAnswerDto>()
                .ForMember(a => a.Content, o => o.MapFrom(a => a.Content))
                .ForMember(a => a.IsCorrect, o => o.MapFrom(a => a.IsCorrect))
                .ReverseMap();
        }


        private void DtoToViewModelMapping()
        {
            this.CreateMap<AnswerDto, TestSystem.Web.Models.TakeTestViewModels.AnswerViewModel>(MemberList.Source);

            this.CreateMap<QuestionDto, TestSystem.Web.Models.TakeTestViewModels.QuestionViewModel>(MemberList.Source);

            this.CreateMap<ExistingTestDto, ExistingTestViewModel>(MemberList.Destination);

            this.CreateMap<TestResultDto, TestResultViewModel>(MemberList.Destination);

            this.CreateMap<TestDto, AdministerTestViewModel>()
                .ForMember(x => x.Duration, option => option.MapFrom(o => o.Duration))
                .ForMember(t => t.TestName, o => o.MapFrom(x => x.TestName))
                .ForMember(t => t.Category, o => o.MapFrom(x => x.Category.Name))
                .ForMember(t => t.Questions, o => o.MapFrom(x => x.Questions))
                .MaxDepth(3)
                .ReverseMap();

            this.CreateMap<QuestionDto, TestSystem.Web.Models.TakeTestViewModels.QuestionViewModel>()
                .ForMember(q => q.Answers, o => o.MapFrom(x => x.Answers))
                .MaxDepth(3)
                .ReverseMap();
        }


        private void DtoToDataModelMapping()
        {
            this.CreateMap<AdministerTestDto, Test>()
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions));

            this.CreateMap<AdministerQuestionDto, Question>(MemberList.Source);

            this.CreateMap<AdministerAnswerDto, Answer>(MemberList.Source);

            this.CreateMap<UserTestDto, UserTest>(MemberList.Source)
                .ForMember(db => db.AnsweredQuestions, o => o.MapFrom(dto => dto.AnsweredQuestions))
                .MaxDepth(3);

            this.CreateMap<AnsweredQuestionDto, AnsweredQuestion>(MemberList.Source)
                .ForMember(db => db.Id, options => options.MapFrom(dto => dto.Id));
        }


        private void DataModelToDtoMapping()
        {
            this.CreateMap<Test, AdministerTestDto>()
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions))
                .ForMember(t => t.Duration, o => o.MapFrom(t => t.Duration.Minutes))
                .ForMember(t => t.Category, o => o.MapFrom(t => t.Category.Name));

            this.CreateMap<Test, EditTestDto>()
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions))
                .ForMember(t => t.Duration, o => o.MapFrom(t => t.Duration.Minutes))
                .ForMember(t => t.Category, o => o.MapFrom(t => t.Category.Name))
                .ForMember(t => t.Id, o => o.MapFrom(t => t.Id.ToString()));

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
                .ForMember(dto => dto.ExecutionTime, o => o.MapFrom(dm => dm.SubmittedOn.Value.Subtract(dm.CreatedOn)))
                .ForMember(dto => dto.Result, o => o.MapFrom(dm => dm.Score));

            this.CreateMap<Test, TestDto>(MemberList.Source)
                .ForMember(t => t.Questions, o => o.MapFrom(x => x.Questions))
                .ForMember(t => t.CategoryId, o => o.MapFrom(x => x.CategoryId.ToString()))
                .ForMember(t => t.Id, o => o.MapFrom(x => x.Id.ToString()))
                .MaxDepth(3);

            this.CreateMap<Question, QuestionDto>()
                .ForMember(dto => dto.Answers, options => options.MapFrom(q => q.Answers));

            this.CreateMap<Question, QuestionDto>(MemberList.Source)
                .ForMember(q => q.Answers, o => o.MapFrom(x => x.Answers))
                .MaxDepth(3);

            this.CreateMap<Category, CategoryDto>()
                .ForMember(dto => dto.Tests, db => db.MapFrom(x => x.Tests))
                .ForMember(dto => dto.Id, db => db.MapFrom(x => x.Id.ToString()))
                .MaxDepth(5);

            this.CreateMap<Question, QuestionDto>()
                .ForMember(q => q.Answers, o => o.MapFrom(x => x.Answers))
                .MaxDepth(3);

            this.CreateMap<Answer, AnswerDto>()
                .ForMember(dto => dto.AnsweredQuestions, o => o.MapFrom(x => x.AnsweredQuestions))
                .MaxDepth(3)
                .ReverseMap();
        }
    }
}
