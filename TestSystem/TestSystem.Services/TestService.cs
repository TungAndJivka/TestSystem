using Bytes2you.Validation;
using Microsoft.EntityFrameworkCore;
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
        private readonly IEfGenericRepository<User> userRepo;
        private readonly IEfGenericRepository<Category> categoryRepo;
        private readonly IEfGenericRepository<Test> testRepo;

        public TestService(IEfGenericRepository<Test> testRepo, IEfGenericRepository<User> userRepo,IEfGenericRepository<Category> categoryRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            this.testRepo = testRepo;
            this.userRepo = userRepo;
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
            var category = this.categoryRepo.All.Where(c => c.Name == testDto.Category).Select(c => c.Id).SingleOrDefault();
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

    }
}
