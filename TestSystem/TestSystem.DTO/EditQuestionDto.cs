using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class EditQuestionDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public ICollection<EditAnswerDto> Answers { get; set; }
    }
}
