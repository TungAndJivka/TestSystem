using System;
using System.Collections.Generic;

namespace TestSystem.Web.Models.TakeTestViewModels
{
    public class ResultViewModel
    {
        public string UserId { get; set; }

        public string TestId { get; set; }

        public string TestName { get; set; }

        public TimeSpan Duration { get; set; }

        public string CategoryName { get; set; }

        public IList<string> Answers { get; set; }

        public DateTime StartedOn { get; set; }
    }
}
