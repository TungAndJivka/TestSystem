using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class Test : DataModel
    {
        [Required]
        public string TestName { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }  //NP
        
        public ICollection<Question> Questions { get; set; }

        [Required]
        public string Status { get; set; }

        public ICollection<Result> Results { get; set; } //NP
    }
}
