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
    public class ResultService : AbstractService, IResultService
    {
        private IEfGenericRepository<UserTest> userTestRepo;

        public ResultService(IEfGenericRepository<UserTest> resultRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(resultRepo, "resultRepo").IsNull().Throw();
            this.userTestRepo = resultRepo;
        }

        public IEnumerable<UserTestDto> GetAll()
        {
            var entities = this.userTestRepo.All;
            var results = this.Mapper.ProjectTo<UserTestDto>(entities);
            return results;
        }

        public UserTestDto GetUserTest(string userId, string testId)
        {
            var entity = userTestRepo.All
                .Where(x => x.UserId == userId && x.TestId.ToString() == testId)
                .FirstOrDefault();

            UserTestDto result = new UserTestDto()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                TestId = entity.TestId.ToString(),
                Test = Mapper.MapTo<TestDto>(entity.Test),
                Score = entity.Score,
                StartTime = entity.CreatedOn,
                SubmittedOn = entity.SubmittedOn,
                AnsweredQuestions = Mapper.ProjectTo<AnsweredQuestionDto>(entity.AnsweredQuestions.AsQueryable()).ToList()
            };

            return result;
        }

        public void AddResult(UserTestDto result)
        {
            UserTest userTest = Mapper.MapTo<UserTest>(result);
            userTestRepo.Add(userTest);
        }

        public void Update(UserTestDto result)
        {
            var entity = userTestRepo.All.Where(r => r.Id.ToString() == result.Id.ToString()).FirstOrDefault();

            var answeredQuestions = Mapper.ProjectTo<AnsweredQuestion>(result.AnsweredQuestions.AsQueryable());

            entity.Score = result.Score;
            entity.SubmittedOn = result.SubmittedOn;
            entity.AnsweredQuestions = answeredQuestions.ToList();

            userTestRepo.Update(entity);

            Saver.SaveChanges();
        }

        public int CheckForTakenTest(string userId, string categoryName)
        {
            var userTest = userTestRepo.All.Where(x => x.UserId == userId && x.Test.Category.Name == categoryName).FirstOrDefault();

            if (userTest == null)
            {
                return 1; // => GetRandomTest
            }

            if (userTest != null && userTest.SubmittedOn == null)
            {
                return 2; // => GetTheSameTest
            }

            return 3; // => RedirectToDashboard
        }

        public IEnumerable<TestDto> GetUserResults(string userId)
        {
            var results = userTestRepo.All.Where(r => r.UserId == userId).Include(r => r.Test).ThenInclude(t => t.Category);

            var entities = new List<Test>();
            foreach (var r in results)
            {
                entities.Add(r.Test);
            }

            var result = this.Mapper.ProjectTo<TestDto>(entities.AsQueryable());
            return result;
        }

        public TestDto GetTestFromCategory(string userId, string categoryName)
        {
            Test testEntity = userTestRepo.All
                .Where(r => r.UserId == userId)
                .Include(r => r.Test)
                .ThenInclude(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .Where(r => r.Test.Category.Name == categoryName)
                .FirstOrDefault()
                .Test;

            var testDto = Mapper.MapTo<TestDto>(testEntity);
            return testDto;
        }

        public IEnumerable<TestResultDto> GetAllResults()
        {
            var entities = this.userTestRepo.All;
            var results = entities
                .Select(x => new TestResultDto
            {
                ExecutionTime = (x.SubmittedOn.Value - x.StartTime)
            });
            return results;
        }

        public IEnumerable<TestResultDto> GetTestResultsForDashBoard()
        {
            var results = this.userTestRepo.All
                .Include(ut => ut.User)
                .Include(ut => ut.Test)
                .ThenInclude(t => t.Category);

            return this.Mapper.ProjectTo<TestResultDto>(results);
        }
    }
}
