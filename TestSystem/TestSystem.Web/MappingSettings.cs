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




            this.CreateMap<QuestionViewModel, QuestionDto>(MemberList.Source);

            this.CreateMap<AnswerViewModel, AnswerDto>(MemberList.Source);

            this.CreateMap<Test, TestDto>(MemberList.Source)
             .ForMember(q => q.Questions, o => o.MapFrom(x => x.Questions))
             .MaxDepth(3)
             .ReverseMap();

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
                .ForMember(t => t.TestName, o => o.MapFrom(t => t.TestName))
                .ForMember(t => t.Duration, o => o.MapFrom(t => TimeSpan.FromMinutes(t.Duration)))
                .ForMember(t => t.Questions, o => o.MapFrom(t => t.Questions))
                .ReverseMap()
                .ForMember(t => t.Category, o => o.MapFrom(t => t.Category.Name));
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
