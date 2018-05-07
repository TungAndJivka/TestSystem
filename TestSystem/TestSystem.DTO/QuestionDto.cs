using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class QuestionDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public ICollection<AnswerDto> Answers { get; set; }

        public string TestId { get; set; }

        public TestDto Test { get; set; }
    }
}
