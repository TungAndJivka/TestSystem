using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface ITestService
    {
        IEnumerable<TestDto> GetAll();

        TestDto GetFullTestInfo(string testId);

        int GetQuestionsCount(string testId);

        TestDto GetRandomTestByCategory(string categoryName);
    }
}
