using Bytes2you.Validation;
using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly IEfGenericRepository<Question> questionRepo;
        private readonly IAnswerService answerService;

        public QuestionService(IEfGenericRepository<Question> questionRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random, IAnswerService answerService)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(questionRepo, "questionRepo").IsNull().Throw();
            Guard.WhenArgument(answerService, "answerService").IsNull().Throw();
            this.questionRepo = questionRepo;
            this.answerService = answerService;
        }

        public IEnumerable<QuestionDto> GetAllQuestionsByTestId(string testId)
        {
            Guard.WhenArgument(testId, "testId").IsNullOrEmpty().Throw();

            var entities = questionRepo.All.Where(q => q.TestId.ToString().Equals(testId)).Include(q => q.Answers);
            var result = this.Mapper.ProjectTo<QuestionDto>(entities);
            return result;
        }

        public Guid AddQuestion(EditQuestionDto questionDto, Guid testId)
        {
            var question = new Question()
            {
                Id = Guid.NewGuid(),
                Description = questionDto.Description,
                TestId = testId,
                CreatedOn = DateTime.Now
            };

            this.questionRepo.Add(question);
            return question.Id;
        }

        public void EditQuestion(EditQuestionDto questionDto, Guid testId)
        {
            Guard.WhenArgument(questionDto, "questionDto").IsNull().Throw();

            if (questionDto.Id == null)
            {
                Guid questionId = this.AddQuestion(questionDto, testId);
                foreach (var answer in questionDto.Answers)
                {
                    this.answerService.AddAnswer(answer, questionId);
                }
            }
            else
            {
                var entity = questionRepo.All
                    .Where(q => q.Id.ToString().Equals(questionDto.Id))
                    .Include(q => q.Answers)
                    .FirstOrDefault();

                foreach (var answer in entity.Answers)
                {
                    if (questionDto.Answers.Any(x => x.Id == answer.Id.ToString()))
                    {
                        continue;
                    }
                    this.answerService.DeleteAnswer(answer);
                }

                foreach (var answer in questionDto.Answers)
                {
                    this.answerService.EditAnswer(answer, entity.Id);
                }

                entity.Description = questionDto.Description;
                entity.ModifiedOn = DateTime.Now;

                this.questionRepo.Update(entity);
            }
        }

        public void DeleteQuestion(Question entity)
        {
            Guard.WhenArgument(entity, "question").IsNull().Throw();

            foreach (var answer in entity.Answers)
            {
                this.answerService.DeleteAnswer(answer);
            }

            this.questionRepo.RealDelete(entity);
        }
    }
}
