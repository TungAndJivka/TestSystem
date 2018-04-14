using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO.Contracts
{
    public interface ITestDto
    {
        string Name { get; set; }
        CategoryDto Category { get; set; }
    }
}
