using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class UserTest : DataModel
    {
        //To Be added Start Time

        [Required]
        public string UserId { get; set; }
        public User User { get; set; } //Np

        [Required]
        public Guid TestId { get; set; }
        public Test Test { get; set; } //NP

        [Required]
        public bool Passed { get; set; }

        [Required]
        public double Score { get; set; }


        public ICollection<AnsweredQuestion> AnsweredQuestions { get; set; } //NP
    }
}
