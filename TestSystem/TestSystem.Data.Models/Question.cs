using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class Question : DataModel
    {
        [Required]
        public string Description { get; set; }

        public ICollection<Answer> Answers { get; set; } //NP

        [Required]
        public Guid TestId { get; set; }
        public Test Test { get; set; } //NP
    }
}
