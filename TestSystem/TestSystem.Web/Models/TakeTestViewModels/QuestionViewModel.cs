using System.Collections.Generic;

namespace TestSystem.Web.Models.TakeTestViewModels
{
    public class QuestionViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public IList<AnswerViewModel> Answers { get; set; }

        public AnswerViewModel SelectedAnswer { get; set; }
    }
}
