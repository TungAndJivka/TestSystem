using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class UserTest : DataModel
    {
        public UserTest()
        {
            this.AnsweredQuestions = new HashSet<AnsweredQuestion>();
        }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public Guid TestId { get; set; }
        public Test Test { get; set; }

        public double? Score { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public ICollection<AnsweredQuestion> AnsweredQuestions { get; set; }
    }
}
