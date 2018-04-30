using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class AdministerTestViewModel
    {
        public string TestName { get; set; }

        public int Duration { get; set; }

        public string Category { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
