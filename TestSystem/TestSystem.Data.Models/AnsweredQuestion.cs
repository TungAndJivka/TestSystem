using System;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class AnsweredQuestion : DataModel
    {
        [Required]
        public Guid UserTestId { get; set; }
        public UserTest UserTest { get; set; }


        [Required]
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
