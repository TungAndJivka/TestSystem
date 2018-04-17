using Bytes2you.Validation;
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
        private IEfGenericRepository<UserTest> UserTestRepo;

        public ResultService(IEfGenericRepository<UserTest> resultRepo, IMappingProvider mapper, ISaver saver)
            : base(mapper, saver)
        {
            Guard.WhenArgument(resultRepo, "resultRepo").IsNull().Throw();
            this.UserTestRepo = resultRepo;
        }

        public IEnumerable<UserTestDto> GetAll()
        {
            var entities = this.UserTestRepo.All.ToList();
            var results = this.Mapper.ProjectTo<UserTestDto>(entities);
            return results;
        }
    }
}
