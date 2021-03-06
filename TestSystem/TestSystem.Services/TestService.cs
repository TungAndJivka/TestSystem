﻿using Bytes2you.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;

namespace TestSystem.Services
{
    public class TestService : AbstractService, ITestService
    {
        private readonly IEfGenericRepository<Category> categoryRepo;
        private readonly IEfGenericRepository<Test> testRepo;
        private readonly IQuestionService questionService;

        public TestService(IEfGenericRepository<Test> testRepo, IEfGenericRepository<Category> categoryRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random, IQuestionService questionService)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            Guard.WhenArgument(categoryRepo, "categoryRepo").IsNull().Throw();
            Guard.WhenArgument(questionService, "questionService").IsNull().Throw();
            this.testRepo = testRepo;
            this.categoryRepo = categoryRepo;
            this.questionService = questionService;
        }

        public IEnumerable<TestDto> GetAll()
        {
            var entities = this.testRepo.All;
            var tests = this.Mapper.ProjectTo<TestDto>(entities);
            return tests;
        }

        public TestDto GetRandomTestByCategory(string categoryName)
        {
            Guard.WhenArgument(categoryName, "categoryName").IsNullOrEmpty().Throw();

            var tests = testRepo.All
                .Include(t => t.Category)
                .Where(t => t.Category.Name == categoryName && t.IsPusblished && (t.IsDisabled ==false))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .ToList();

            int allCount = tests.Count();
            int random = this.Random.Next(0, allCount);

            var dbTest = tests[random];

            var testDto = Mapper.MapTo<TestDto>(dbTest);
            return testDto;
        }

        public TestDto GetFullTestInfo(string testId)
        {
            Guard.WhenArgument(testId, "testId").IsNullOrEmpty().Throw();

            var entity = testRepo.All
                .Where(t => t.Id.ToString().Equals(testId))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();

            var result = this.Mapper.MapTo<TestDto>(entity);
            return result;
        }

        public IEnumerable<ExistingTestDto> AllTestsForDashBoard()
        {
            var tests = this.testRepo.All;

            return this.Mapper.EnumerableProjectTo<Test, ExistingTestDto>(tests);
        }

        public void CreateTest(AdministerTestDto testDto)
        {
            if (testDto == null)
            {
                throw new ArgumentNullException(nameof(testDto));
            }

            Guid category = this.categoryRepo.All
                .Where(c => c.Name == testDto.Category)
                .Select(c => c.Id)
                .SingleOrDefault();

            Test testToBeAdded = new Test()
            {
                TestName = testDto.TestName,
                CategoryId = category,
                Duration = TimeSpan.FromMinutes(testDto.Duration),
                IsPusblished = testDto.IsPusblished,
                Questions = this.Mapper.EnumerableProjectTo<AdministerQuestionDto, Question>(testDto.Questions).ToList()
            };

            this.testRepo.Add(testToBeAdded);
            Saver.SaveChanges();
        }


        public int GetQuestionsCount(string testId)
        {
            Guard.WhenArgument(testId, "testId").IsNullOrEmpty().Throw();

            var test = testRepo.All.Where(t => t.Id.ToString().Equals(testId)).Include(t => t.Questions).FirstOrDefault();
            if (test != null)
            {
                return test.Questions.Count;
            }

            return 0;
        }

        public bool PublishTest(string testId)
        {
            Guard.WhenArgument(testId, "testId").IsNullOrEmpty().Throw();

            var test = this.testRepo.All
               .Where(t => t.Id.ToString() == testId)
               .Include(t => t.Questions)
               .FirstOrDefault();

            if (test.Questions == null || test.Questions.Count < 1)
            {
                return false;
            }

            test.IsPusblished = true;
            this.testRepo.Update(test);
            this.Saver.SaveChanges();
            return true;
        }

        public void DeleteTest(string testName, string categoryName)
        {
            Guard.WhenArgument(testName, "testName").IsNullOrEmpty().Throw();
            Guard.WhenArgument(categoryName, "categoryName").IsNullOrEmpty().Throw();

            var test = this.testRepo.All
               .Include(t => t.Category)
               .Where(t => t.TestName == testName && t.Category.Name == categoryName)
               .FirstOrDefault();

            foreach (var question in test.Questions)
            {
                this.questionService.DeleteQuestion(question);
            }

            test.IsDeleted = true;
            this.testRepo.Update(test);
            this.Saver.SaveChanges();
        }


        public AdministerTestDto GetTest(string testName, string categoryName)
        {
            Guard.WhenArgument(testName, "testName").IsNullOrEmpty().Throw();
            Guard.WhenArgument(categoryName, "categoryName").IsNullOrEmpty().Throw();

            var test = this.testRepo.All
               .Include(t => t.Category)
               .Include(t => t.Questions)
               .ThenInclude(q => q.Answers)
               .Where(t => t.TestName == testName && t.Category.Name == categoryName)
               .FirstOrDefault();

            var testToBeReturned = this.Mapper.MapTo<AdministerTestDto>(test);
            return testToBeReturned;
        }

        public EditTestDto GetTestForEditing(string testName, string categoryName)
        {
            Guard.WhenArgument(testName, "testName").IsNullOrEmpty().Throw();
            Guard.WhenArgument(categoryName, "categoryName").IsNullOrEmpty().Throw();

            var test = this.testRepo.All
               .Include(t => t.Category)
               .Include(t => t.Questions)
               .ThenInclude(q => q.Answers)
               .Where(t => t.TestName == testName && t.Category.Name == categoryName)
               .FirstOrDefault();

            var testToBeReturned = this.Mapper.MapTo<EditTestDto>(test);
            return testToBeReturned;
        }

        public void EditTest(EditTestDto testDto)
        {
            Guard.WhenArgument(testDto, "testDto").IsNull().Throw();

            var entity = testRepo.All
                .Where(t => t.Id.ToString() == testDto.Id)
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();

            foreach (var question in entity.Questions)
            {
                if (testDto.Questions.Any(x => x.Id == question.Id.ToString()))
                {
                    continue;
                }
                this.questionService.DeleteQuestion(question);
            }

            foreach (var question in testDto.Questions)
            {
                this.questionService.EditQuestion(question, entity.Id);
            }

            entity.TestName = testDto.TestName;
            entity.Duration = TimeSpan.FromMinutes(testDto.Duration);
            entity.CategoryId = this.categoryRepo.All.Where(x => x.Name == testDto.Category).FirstOrDefault().Id;
            entity.IsPusblished = testDto.IsPusblished;
            entity.ModifiedOn = DateTime.Now;

            this.testRepo.Update(entity);
            this.Saver.SaveChanges();
        }


        public void DisableTest(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var test = this.testRepo.All
                        .Where(t => t.Id.ToString() == Id)
                        .FirstOrDefault();

            test.IsDisabled = true;

            this.testRepo.Update(test);
            this.Saver.SaveChanges();
        }


        public void EnableTest(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var test = this.testRepo.All
                        .Where(t => t.Id.ToString() == Id)
                        .FirstOrDefault();

            test.IsDisabled = false;

            this.testRepo.Update(test);
            this.Saver.SaveChanges();
        }

    }
}
