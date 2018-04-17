using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class Category : DataModel
    {        
        
        [Required]
        public string Name { get; set; }

        public List<Test> Tests { get; set; } //NP

    }
}