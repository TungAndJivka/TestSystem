using System;
using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class AnswerDto
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public Guid QuestionID { get; set; }
        public QuestionDto Question { get; set; } //NP

        public ICollection<AnsweredQuestionDto> AnsweredQuestions { get; set; }

    }
}