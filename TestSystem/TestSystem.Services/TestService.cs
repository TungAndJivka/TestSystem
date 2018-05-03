using Bytes2you.Validation;
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

        public TestService(IEfGenericRepository<Test> testRepo, IEfGenericRepository<Category> categoryRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            this.testRepo = testRepo;
            this.categoryRepo = categoryRepo;
        }

        public IEnumerable<TestDto> GetAll()
        {
            var entities = this.testRepo.All;
            var tests = this.Mapper.ProjectTo<TestDto>(entities);
            return tests;
        }

        public TestDto GetRandomTestByCategory(string categoryName)
        {
            var tests = testRepo.All
                .Include(t => t.Category)
                .Where(t => t.Category.Name == categoryName && t.IsPusblished)
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
            var entities = testRepo.All
                .Where(t => t.Id.ToString().Equals(testId))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();

            var result = this.Mapper.MapTo<TestDto>(entities);
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

            if( category == default(Guid))
            {

            }

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
            var test = testRepo.All.Where(t => t.Id.ToString().Equals(testId)).Include(t => t.Questions).FirstOrDefault();
            if (test != null)
            {
                return test.Questions.Count;
            }

            return 0;
        }

        public bool PublishTest(string testId)
        {
            if (string.IsNullOrEmpty(testId))
            {
                throw new ArgumentNullException("Id cannot be null!");
            }

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
            if (string.IsNullOrEmpty(testName))
            {
                throw new ArgumentNullException("Name cannot be null!");
            }

            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException("Category Name cannot be null!");
            }

            var test = this.testRepo.All
               .Include(t => t.Category)
               .Where(t => t.TestName == testName && t.Category.Name == categoryName)
               .FirstOrDefault();

            test.IsDeleted = true;
            this.testRepo.Update(test);
            this.Saver.SaveChanges();
        }

        public AdministerTestDto GetTest(string testName, string categoryName)
        {
            var test = this.testRepo.All
               .Include(t => t.Category)               
               .Include(t => t.Questions)
               .ThenInclude(q => q.Answers)
               .Where(t => t.TestName == testName && t.Category.Name == categoryName)
               .FirstOrDefault();

            var testToBeReturned = this.Mapper.MapTo<AdministerTestDto>(test);
            return testToBeReturned;
        }
    }
}
