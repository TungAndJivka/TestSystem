using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class AnsweredQuestion : DataModel
    {
        [Required]
        public Guid UserTestId { get; set; }
        public UserTest UserTest { get; set; }//NP


        [Required]
        public Guid AnswersId { get; set; }
        public Answer Answer { get; set; } //NP
    }
}
