﻿using Bytes2you.Validation;
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

        public QuestionService(IEfGenericRepository<Question> questionRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(questionRepo, "questionRepo").IsNull().Throw();
            this.questionRepo = questionRepo;
        }
    }
}
