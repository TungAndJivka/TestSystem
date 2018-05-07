using System;
using TestSystem.Data.Models;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface IQuestionService
    {
        void EditQuestion(EditQuestionDto questionDto, Guid testId);

        void DeleteQuestion(Question entity);
    }
}
