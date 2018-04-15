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
        private IEfGenericRepository<Result> resultRepo;

        public ResultService(IEfGenericRepository<Result> resultRepo, IMappingProvider mapper, ISaver saver)
            : base(mapper, saver)
        {
            Guard.WhenArgument(resultRepo, "resultRepo").IsNull().Throw();
            this.resultRepo = resultRepo;
        }

        public IEnumerable<ResultDto> GetAll()
        {
            var entities = this.resultRepo.All.ToList();
            var results = this.Mapper.ProjectTo<ResultDto>(entities);
            return results;
        }
    }
}
