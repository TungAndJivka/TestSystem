using Bytes2you.Validation;
using System.Linq;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;

namespace TestSystem.Services
{
    public class AnswerService : AbstractService, IAnswerService
    {
        private IEfGenericRepository<Answer> answerRepo;

        public AnswerService(IEfGenericRepository<Answer> answerRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(answerRepo, "answerRepo").IsNull().Throw();
            this.answerRepo = answerRepo;
        }

        public AnswerDto GetById(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var entity = answerRepo.All.Where(a => a.Id.ToString().Equals(id)).FirstOrDefault();
            var answerDto = Mapper.MapTo<AnswerDto>(entity);
            return answerDto;
        }
    }
}
