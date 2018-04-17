using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class Answer : DataModel
    {
        [Required]
        public string Content { get; set; }

        public Guid QuestionID { get; set; }
        public Question Question { get; set; } //NP

        [Required]
        public bool IsCorrect {get; set;}

        public ICollection<AnsweredQuestion> AnsweredQuestions { get; set; } //NP
    }
}