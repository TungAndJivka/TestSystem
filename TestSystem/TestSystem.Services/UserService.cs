using Bytes2you.Validation;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;

namespace TestSystem.Services
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IEfGenericRepository<User> userRepo;

        public UserService(IEfGenericRepository<User> userRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(userRepo, "userRepo").IsNull().Throw();
            this.userRepo = userRepo;
        }
    }
}
