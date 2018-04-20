using System;
using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface IResultService
    {
        IEnumerable<UserTestDto> GetAll();

        bool AddNewResult(string userId, Guid testId);
    }
}
