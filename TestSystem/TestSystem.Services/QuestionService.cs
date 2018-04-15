using Bytes2you.Validation;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;

namespace TestSystem.Services
{
    public class QuestionService : AbstractService, IQuestionService
    {
        private IEfGenericRepository<Question> questionRepo;

        public QuestionService(IEfGenericRepository<Question> questionRepo, IMappingProvider mapper, ISaver saver)
            : base(mapper, saver)
        {
            Guard.WhenArgument(questionRepo, "questionRepo").IsNull().Throw();
            this.questionRepo = questionRepo;
        }
    }
}
