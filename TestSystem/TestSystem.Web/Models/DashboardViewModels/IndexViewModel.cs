﻿using System.Collections.Generic;
using TestSystem.Web.Models.Shared;

namespace TestSystem.Web.Models.DashboardViewModels
{
    public class IndexViewModel
    {
        public string Title { get; set; }

        public IList<CategoryViewModel> Categories { get; set; }

        public IEnumerable<TestViewModel> Tests { get; set; }

        public int TestsTaken { get; set; }
    }
}
