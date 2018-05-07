using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class EditTestDto
    {
        public string Id { get; set; }

        public string TestName { get; set; }

        public int Duration { get; set; }

        public bool IsPusblished { get; set; }

        public string Category { get; set; }

        public ICollection<EditQuestionDto> Questions { get; set; }
    }
}
