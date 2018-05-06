using Bytes2you.Validation;
using Microsoft.EntityFrameworkCore;
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
    public class QuestionService : AbstractService, IQuestionService
    {
        private IEfGenericRepository<Question> questionRepo;

        public QuestionService(IEfGenericRepository<Question> questionRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(questionRepo, "questionRepo").IsNull().Throw();
            this.questionRepo = questionRepo;
        }

        public IEnumerable<QuestionDto> GetAllQuestionsByTestId(string testId)
        {
            Guard.WhenArgument(testId, "testId").IsNullOrEmpty().Throw();

            var entities = questionRepo.All.Where(q => q.TestId.ToString().Equals(testId)).Include(q => q.Answers);
            var result = this.Mapper.ProjectTo<QuestionDto>(entities);
            return result;
        }
    }
}
