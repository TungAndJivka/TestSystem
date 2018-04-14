using System;
using System.Collections.Generic;
using System.Text;
using TestSystem.DTO.Contracts;

namespace TestSystem.DTO
{
    public class TestDto : ITestDto
    {
        public string Name { get; set; } // TestName

        public TimeSpan Duration { get; set; }

        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }  //NP

        public ICollection<QuestionDto> Questions { get; set; }

        public string Status { get; set; }
    }
}
