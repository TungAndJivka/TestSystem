using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class Test : DataModel
    {
        public Test()
        {
            this.Questions = new HashSet<Question>();
            this.UserTests = new HashSet<UserTest>();
        }

        [Required]
        public string TestName { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }  //NP
        
        public ICollection<Question> Questions { get; set; }

        [Required]
        public bool IsPusblished { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        public ICollection<UserTest> UserTests { get; set; } //NP
    }
}
