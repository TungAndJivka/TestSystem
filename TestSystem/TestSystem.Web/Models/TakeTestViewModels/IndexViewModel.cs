using System;
using System.Collections.Generic;

namespace TestSystem.Web.Models.TakeTestViewModels
{
    public class IndexViewModel // TakeTest
    {
        public string UserId { get; set; }

        public string TestId { get; set; }

        public string TestName { get; set; }

        public TimeSpan Duration { get; set; }

        public string CategoryName { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }

        public DateTime StartedOn { get; set; }

    }
}
