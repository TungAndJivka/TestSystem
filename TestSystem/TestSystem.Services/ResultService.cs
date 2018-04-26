using Bytes2you.Validation;
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

            return Mapper.MapTo<UserTestDto>(entity);
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
    }
}
