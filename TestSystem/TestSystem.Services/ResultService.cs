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

        public bool AddNewResult(string userId, Guid testId)
        {
            try
            {
                this.userTestRepo.Add(new UserTest() { UserId = userId, TestId = testId, });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TestResultDto> GetAllResults()
        {
            var entities = this.userTestRepo.All;
            var results = entities.Select(x => new TestResultDto
            {
                ExecutionTime = x.SubmittedOn.Value - x.StartTime.Value
            });
            return results;
        }

        public IEnumerable<TestResultDto> GetAllTestResults()
        {
            var results = this.userTestRepo.All
                .Include(ut => ut.User)
                .Include(ut => ut.Test)
                .ThenInclude(t => t.Category);

            return this.Mapper.ProjectTo<TestResultDto>(results);
        }
    }
}
