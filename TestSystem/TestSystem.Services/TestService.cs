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
        private readonly IEfGenericRepository<Test> testRepo;

        public TestService(
            IEfGenericRepository<Test> testRepo, 
            IMappingProvider mapper,
            ISaver saver, 
            IRandomProvider random)

            : base(mapper, saver, random)
        {
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            this.testRepo = testRepo;
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
