using System;

namespace TestSystem.DTO
{
    public class AnswerDto
    {
        public string Content { get; set; }

        public Guid QuestionID { get; set; }
        public QuestionDto Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}