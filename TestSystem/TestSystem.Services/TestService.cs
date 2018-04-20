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
        private IEfGenericRepository<Test> testRepo;
        private readonly IEfGenericRepository<User> userRepo;

        public TestService(IEfGenericRepository<Test> testRepo, IEfGenericRepository<User> userRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            this.testRepo = testRepo;
            this.userRepo = userRepo;
        }

        public IEnumerable<TestDto> GetAll()
        {
            var entities = this.testRepo.All.ToList();
            var tests = this.Mapper.ProjectTo<TestDto>(entities.AsQueryable());
            return tests;
        }

        public IEnumerable<TestDto> GetUserTests(string id)
        {
            var entities = userRepo.All.Include(u => u.Tests).Where(u => u.Id == id).SelectMany(u => u.Tests);
            var result = this.Mapper.ProjectTo<TestDto>(entities);
            return result;
        }

        public TestDto GetRandomTestByCategory(string categoryName)
        {
            int allCount = this.testRepo.All.Where(t => t.Category.Name == categoryName).Count();
            int skip = this.Random.Next(0, allCount - 1);
            var dbTest = testRepo.All.Where(t => t.Category.Name == categoryName).Skip(skip).Take(1);
            var testDto = Mapper.MapTo<TestDto>(dbTest);
            return testDto;
        }   
                             
        public TestDto GetFullTestInfo(string testId)
        {
            var entities = testRepo.All.Where(t => t.Id.Equals(testId)).Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefault();
            var result = this.Mapper.MapTo<TestDto>(entities);
            return result;
        }
    }
}
