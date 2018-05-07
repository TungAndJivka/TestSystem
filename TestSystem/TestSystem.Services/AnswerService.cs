using Bytes2you.Validation;
using System;
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

        public void AddAnswer(EditAnswerDto answerDto, Guid questionId)
        {
            var answer = new Answer()
            {
                Id = Guid.NewGuid(),
                Content = answerDto.Content,
                IsCorrect = answerDto.IsCorrect,
                QuestionID = questionId,
                CreatedOn = DateTime.Now
            };

            this.answerRepo.Add(answer);
        }

        public void EditAnswer(EditAnswerDto answerDto, Guid questionId)
        {
            Guard.WhenArgument(answerDto, "answerDto").IsNull().Throw();

            if (answerDto.Id == null)
            {
                this.AddAnswer(answerDto, questionId);
            }
            else
            {
                var entity = answerRepo.All
                    .Where(a => a.Id.ToString() == answerDto.Id)
                    .FirstOrDefault();

                if (entity == null)
                {
                    this.AddAnswer(answerDto, questionId);
                }
                else
                {
                    entity.Content = answerDto.Content;
                    entity.IsCorrect = answerDto.IsCorrect;
                    this.answerRepo.Update(entity);
                }
            }
        }


        public void DeleteAnswer(Answer entity)
        {
            Guard.WhenArgument(entity, "question").IsNull().Throw();
            this.answerRepo.RealDelete(entity);
        }
    }
}
