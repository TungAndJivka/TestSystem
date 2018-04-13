using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestSystem.Data.Models
{
    public class Test
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TestName { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }  //NP
        
        public List<Question> Questions { get; set; }

        [Required]
        public string Status { get; set; }
        
        [Required]
        public bool IsDeleted { get; set; }

    }
}
