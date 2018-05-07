using System;
using TestSystem.Data.Models;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface IAnswerService
    {
        AnswerDto GetById(string id);

        void EditAnswer(EditAnswerDto answerDto, Guid questionId);

        void AddAnswer(EditAnswerDto answerDto, Guid questionId);

        void DeleteAnswer(Answer entity);
    }
}
