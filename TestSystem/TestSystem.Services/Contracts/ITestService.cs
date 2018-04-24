using System;
using System.Collections.Generic;
using System.Text;
using TestSystem.Data.Models;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface ITestService
    {
        IEnumerable<TestDto> GetAll();

        IEnumerable<TestDto> GetUserTests(string Id);

        TestDto GetFullTestInfo(string testId);

        int GetQuestionsCount(string testId);

        TestDto GetRandomTestByCategory(string categoryName);
    }
}
