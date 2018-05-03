using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class QuestionViewModel
    {

        [Required(ErrorMessage = "Question description is required!")]
        [StringLength(500, ErrorMessage = "Question can't be more than 500 symbols")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Answers are required")]
        public IList<AnswerViewModel> Answers {get;set;}
    }
}
