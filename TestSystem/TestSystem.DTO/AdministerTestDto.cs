using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO
{
    public class AdministerTestDto
    {
        public string Id { get; set; }
        public string TestName { get; set; }
        public int Duration { get; set; }
        public bool IsPusblished { get; set; }

        public string Category { get; set; }

        public ICollection<AdministerQuestionDto> Questions { get; set; }
    }
}
