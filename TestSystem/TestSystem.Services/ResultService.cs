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
            Guard.WhenArgument(userId, "userId").IsNullOrEmpty().Throw();
            Guard.WhenArgument(testId, "testId").IsNullOrEmpty().Throw();

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
            Guard.WhenArgument(result, "result").IsNull().Throw();

            UserTest userTest = Mapper.MapTo<UserTest>(result);
            userTestRepo.Add(userTest);
            Saver.SaveChanges();
        }

        public void Update(UserTestDto result)
        {
            Guard.WhenArgument(result, "result").IsNull().Throw();

            var entity = userTestRepo.All.Where(r => r.Id.ToString() == result.Id.ToString()).FirstOrDefault();

            var answeredQuestions = Mapper.ProjectTo<AnsweredQuestion>(result.AnsweredQuestions.AsQueryable());

            entity.Score = result.Score;
            entity.SubmittedOn = result.SubmittedOn;
            entity.AnsweredQuestions = answeredQuestions.ToList();

            userTestRepo.Update(entity);

            Saver.SaveChanges();
        }

        public StatusType CheckForTakenTest(string userId, string categoryName)
        {
            Guard.WhenArgument(userId, "userId").IsNullOrEmpty().Throw();
            Guard.WhenArgument(categoryName, "categoryName").IsNullOrEmpty().Throw();

            var userTest = userTestRepo.All.Where(x => x.UserId == userId && x.Test.Category.Name == categoryName).FirstOrDefault();

            if (userTest == null)
            {
                return StatusType.TestNotStarted; // => GetRandomTest
            }

            if (userTest != null && userTest.SubmittedOn == null)
            {
                return StatusType.TestNotSubmitted; // => GetTheSameTest
            }

            return StatusType.TestSubmitted; // => RedirectToDashboard
        }

        public IEnumerable<TestDto> GetUserResults(string userId)
        {
            Guard.WhenArgument(userId, "userId").IsNullOrEmpty().Throw();

            var results = userTestRepo.All
                .Where(r => r.UserId == userId)
                .Include(r => r.Test)
                .ThenInclude(t => t.Category)
                .Where(t => t.SubmittedOn != null);

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
            Guard.WhenArgument(userId, "userId").IsNullOrEmpty().Throw();
            Guard.WhenArgument(categoryName, "categoryName").IsNullOrEmpty().Throw();

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
                ExecutionTime = (x.SubmittedOn.Value - x.CreatedOn)
            });
            return results;
        }

        public IEnumerable<TestResultDto> GetTestResultsForDashBoard()
        {
            var results = this.userTestRepo.All
                .Where(r => r.SubmittedOn != null && r.Score != null)
                .Include(ut => ut.User)
                .Include(ut => ut.Test)
                .ThenInclude(t => t.Category);

            return this.Mapper.ProjectTo<TestResultDto>(results);
        }

        public int GetTestsTaken()
        {
            return this.userTestRepo.All.Count();
        }
    }
}
