using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO
{
    public class UserTestDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; }

        public Guid TestId { get; set; }
        public TestDto Test { get; set; }

        public double? Score { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? SubmittedOn { get; set; }

        //public ICollection<AnsweredQuestionDto> AnsweredQuestions { get; set; }
    }
}
