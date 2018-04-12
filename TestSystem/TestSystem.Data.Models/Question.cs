using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestSystem.Data.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Answer> Answers { get; set; } //NP

        [Required]
        public Guid TestId { get; set; }
        public Test Test { get; set; } //NP

        [Required]
        public bool IsDeleted { get; set; }

    }
}
