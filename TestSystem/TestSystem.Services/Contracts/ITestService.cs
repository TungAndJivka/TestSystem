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

        bool PublishTest(string testId);

        void DeleteTest(string testName, string categoryName);

        AdministerTestDto GetTest(string testName, string categoryName);
    }
}
