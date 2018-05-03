using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class AdministerTestViewModel
    {

        public string Id { get; set; }


        [Required(ErrorMessage = "Test name is required")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "The Test name should be between 4 and 30 symbols long")]
        public string TestName { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(5,300, ErrorMessage = "Duration should be atleast 5 minutes")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Question is required")]
        public IList<QuestionViewModel> Questions { get; set; }
    }
}
