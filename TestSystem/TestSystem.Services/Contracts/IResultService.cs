using System;
using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface IResultService
    {
        IEnumerable<UserTestDto> GetAll();

        void AddResult(UserTestDto result);

        void Update(UserTestDto result);

        StatusType CheckForTakenTest(string userId, string testId);

        UserTestDto GetUserTest(string userId, string testId);

        IEnumerable<TestDto> GetUserResults(string userId);

        TestDto GetTestFromCategory(string userId, string categoryName);

        IEnumerable<TestResultDto> GetTestResultsForDashBoard();

        int GetTestsTaken();
    }
}
