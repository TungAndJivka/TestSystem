using System.ComponentModel.DataAnnotations;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class AnswerViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Answer description is required!")]
        [StringLength(500, ErrorMessage = "Answers content's length must be maximum 500 symbols!")]
        public string Content { get; set; }

        public bool IsCorrect { get; set; }
    }
}
