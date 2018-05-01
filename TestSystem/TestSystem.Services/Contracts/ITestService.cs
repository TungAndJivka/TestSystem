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

        IEnumerable<ExistingTestDto> AllTestsForDashBoard();

        void CreateTest(AdministerTestDto testDto);

        void PublishTest(string testName, string categoryName);
        void DeleteTest(string testName, string categoryName);
    }
}
