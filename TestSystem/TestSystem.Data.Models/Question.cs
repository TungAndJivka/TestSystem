using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class Question : DataModel
    {             
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        [Required]
        public string Description { get; set; }

        public ICollection<Answer> Answers { get; set; }

        [Required]
        public Guid TestId { get; set; }
        public Test Test { get; set; }
    }
}
