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

        public TestService(IEfGenericRepository<Test> testRepo, IEfGenericRepository<User> userRepo, IMappingProvider mapper, ISaver saver)
            : base(mapper, saver)
        {
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            this.testRepo = testRepo;
            this.userRepo = userRepo;
        }

        public IEnumerable<TestDto> GetUserTests(string id)
        {
            var entities = userRepo.All.Include(u => u.Tests).Where(u => u.Id == id).SelectMany(u => u.Tests).ToArray();
            var result = this.Mapper.ProjectTo<TestDto>(entities);
            return result;
        }

        //public TestDto GetOneRandomTestByCategory(string categoryName)
        //{
        //    var max = this.testRepo.All.Where(t => t.Category.Name == categoryName).Count();
        //    int rnd = this.Random.Next(0, max - 1);
        //    var test = testRepo.All.Skip(rnd).FirstOrDefault();

        //    var result = this.Mapper.MapTo<TestDto>(test);
        //    return result;
        //}
    }
}
