﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class Category : DataModel
    {
        public Category()
        {
            this.Tests = new HashSet<Test>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Test> Tests { get; set; }

    }
}