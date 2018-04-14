using System;
using System.Collections.Generic;
using System.Text;
using TestSystem.DTO.Contracts;

namespace TestSystem.DTO
{
    public class TestDto : ITestDto
    {
<<<<<<< HEAD
        public string Name { get; set; } // TestName
=======
        public string Name { get; set; }



        public string TestName { get; set; }
>>>>>>> b835627e5f9e8ccf88b6a132b79b91f64f9b7f11

        public TimeSpan Duration { get; set; }

        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }  //NP

        public ICollection<QuestionDto> Questions { get; set; }

        public string Status { get; set; }
    }
}
