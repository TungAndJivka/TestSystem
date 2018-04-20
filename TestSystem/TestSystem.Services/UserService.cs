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

        public UserService(IMappingProvider mapper, ISaver saver, IEfGenericRepository<User> userRepo, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(userRepo, "userRepo").IsNull().Throw();
            this.userRepo = userRepo;
        }

        public UserDto GetUserByIdWithTests(string userId)
        {
            var entity = userRepo.All.Where(u => u.Id == userId).Include(u => u.UserTests).FirstOrDefault();
            var userDto = Mapper.MapTo<UserDto>(entity);
            return userDto;
        }

    }
}
