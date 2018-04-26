using System;

namespace TestSystem.DTO
{
    public class AnsweredQuestionDto
    {
        public Guid Id { get; set; }

        public Guid UserTestId { get; set; }

        public Guid AnswerId { get; set; }
    }
}