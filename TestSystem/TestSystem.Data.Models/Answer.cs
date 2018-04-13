using System;
using System.ComponentModel.DataAnnotations;

namespace TestSystem.Data.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid QuestionID { get; set; }
        public Question Question { get; set; } //NP

        [Required]
        public bool IsCorrect {get; set;}

        [Required]
        public bool IsDeleted { get; set; }
    }
}