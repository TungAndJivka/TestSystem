using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class QuestionViewModel
    {
        public string Description { get; set; }

        ICollection<AnswerViewModel> Answers {get;set;}
    }
}
