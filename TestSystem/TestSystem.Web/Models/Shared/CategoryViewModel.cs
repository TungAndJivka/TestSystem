using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Models.Shared
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public ICollection<TestViewModel> Tests { get; set; }
    }
}
