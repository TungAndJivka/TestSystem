using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Models.TakeTestViewModels
{
    public class ResultViewModel
    {
        public string UserId { get; set; } // not from Test

        public string TestId { get; set; } // Id

        public string TestName { get; set; }

        public TimeSpan Duration { get; set; }

        public string CategoryName { get; set; } // not from Test

        public IList<string> Answers { get; set; }

        public DateTime StartedOn { get; set; }
    }
}
