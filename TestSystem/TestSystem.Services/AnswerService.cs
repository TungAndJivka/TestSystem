using Bytes2you.Validation;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
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
    }
}
