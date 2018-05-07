using System;
using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class TestDto
    {
        public string Id { get; set; }

        public string TestName { get; set; } 

        public TimeSpan Duration { get; set; }

        public bool IsPusblished { get; set; }

        public bool IsDisabled { get; set; }

        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public ICollection<QuestionDto> Questions { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
