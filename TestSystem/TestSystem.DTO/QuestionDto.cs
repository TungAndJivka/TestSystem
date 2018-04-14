using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO
{
    public class QuestionDto
    {
        public string Description { get; set; }

        public ICollection<AnswerDto> Answers { get; set; }

        public Guid TestId { get; set; }
        public TestDto Test { get; set; }
    }
}
