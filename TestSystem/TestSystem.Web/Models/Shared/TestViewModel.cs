using System;
using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Web.Models.Shared
{
    public class TestViewModel
    {
        public string Name { get; set; }

        public bool IsSubmitted { get; set; }

        public string CategoryName { get; set; }
    }
}