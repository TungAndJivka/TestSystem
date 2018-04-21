using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.DashboardViewModels
{
    public class IndexViewModel
    {
        public string Title { get; set; }

        public IEnumerable<ResultViewModel> Results { get; set; }

        public IEnumerable<TestViewModel> Tests { get; set; }
    }
}
