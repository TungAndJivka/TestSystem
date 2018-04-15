using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Web.Models.DashboardViewModels
{
    public class IndexViewModel
    {
        public string Title { get; set; }

        public IList<CategoryDto> Categories { get; set; } // TODO IEnumerable<CategoryViewModel>

        public IList<TestDto> Tests { get; set; } // TODO IEnumerable<TestViewModel>
    }
}
