using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO
{
    public class TestDto
    {
        public string Id { get; set; }
        public string TestName { get; set; } 
        public TimeSpan Duration { get; set; }
        public bool IsPusblished { get; set; }

        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }  //NP

        public ICollection<QuestionDto> Questions { get; set; }

        //public ICollection<UserTestDto> UserTests { get; set; } //NP
    }
}
