using System;
using System.Collections.Generic;

namespace TestSystem.Web.Models.TakeTestViewModels
{
    public class IndexViewModel // TakeTest
    {
        public string UserId { get; set; } // not from Test

        public string TestId { get; set; } // Id

        public string TestName { get; set; }

        public TimeSpan Duration { get; set; }

        public string CategoryName { get; set; } // not from Test
        //public string CategoryId { get; set; }
        //public CategoryDto Category { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }

    }
}
