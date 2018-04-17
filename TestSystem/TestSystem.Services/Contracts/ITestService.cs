using System;
using System.Collections.Generic;
using System.Text;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface ITestService
    {
        IEnumerable<TestDto> GetUserTests(string Id);
    }
}
