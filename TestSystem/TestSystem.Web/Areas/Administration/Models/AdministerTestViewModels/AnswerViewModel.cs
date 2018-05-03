using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Answer description is required!")]
        [StringLength(500, ErrorMessage = "Answers content's length must be maximum 500 symbols!")]
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
