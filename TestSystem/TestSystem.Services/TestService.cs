using Bytes2you.Validation;
using System;
using System.Collections.Generic;
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

        public TestService(IEfGenericRepository<Test> testRepo, IMappingProvider mapper, ISaver saver)
            : base(mapper, saver)
        {
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            this.testRepo = testRepo;
        }

        public IEnumerable<TestDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestDto> GetAllByCategory()
        {
            throw new NotImplementedException();
        }
    }
}
