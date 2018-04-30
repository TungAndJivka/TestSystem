using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class AdministerQuestionDto
    {
        public string Description { get; set; }

        public ICollection<AdministerAnswerDto> Answers { get; set; }
    }
}