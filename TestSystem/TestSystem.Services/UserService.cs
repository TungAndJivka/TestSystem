using Bytes2you.Validation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;

namespace TestSystem.Services
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IEfGenericRepository<User> userRepo;
        private IEfGenericRepository<Test> testRepo;
        private IEfGenericRepository<UserTest> resultRepo;

        public UserService(IMappingProvider mapper, ISaver saver, IEfGenericRepository<User> userRepo, IEfGenericRepository<Test> testRepo, IEfGenericRepository<UserTest> resultRepo, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(userRepo, "userRepo").IsNull().Throw();
            Guard.WhenArgument(testRepo, "testRepo").IsNull().Throw();
            this.userRepo = userRepo;
            this.resultRepo = resultRepo;
            this.testRepo = testRepo;
        }

        public UserDto GetUserByIdWithTests(string userId)
        {
            var entity = userRepo.All.Where(u => u.Id == userId).Include(u => u.UserTests).FirstOrDefault();
            var userDto = Mapper.MapTo<UserDto>(entity);
            return userDto;
        }

        public TestDto GetTestFromCategory(string userId, string categoryName)
        {
            Test testEntity = resultRepo.All
                .Where(x => x.UserId == userId)
                .Include(x => x.Test)
                .ThenInclude(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .Where(t => t.Test.Category.Name == categoryName)                
                .FirstOrDefault()
                .Test;

            var testDto = Mapper.MapTo<TestDto>(testEntity);
            return testDto;
        }

    }
}
