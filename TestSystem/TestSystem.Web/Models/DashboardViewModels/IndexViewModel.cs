using System.Collections.Generic;
using TestSystem.DTO;
using TestSystem.Web.Models.Shared;

namespace TestSystem.Web.Models.DashboardViewModels
{
    public class IndexViewModel
    {
        public string Title { get; set; }

        public IList<CategoryViewModel> Categories { get; set; } // TODO IEnumerable<CategoryViewModel>
        //public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<TestViewModel> Tests { get; set; } // TODO IEnumerable<TestViewModel>
    }
}
