using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class QuestionViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Question description is required!")]
        [StringLength(500, ErrorMessage = "Question can't be more than 500 symbols")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Answers are required")]
        public IList<AnswerViewModel> Answers {get;set;}
    }
}
