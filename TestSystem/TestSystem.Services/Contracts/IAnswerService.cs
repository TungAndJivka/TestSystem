using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface IAnswerService
    {
        AnswerDto GetById(string id);
    }
}
