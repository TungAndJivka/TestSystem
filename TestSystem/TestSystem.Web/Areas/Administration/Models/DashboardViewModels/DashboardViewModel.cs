using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.DashboardViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.ExistingTests = new HashSet<ExistingTestViewModel>();
            this.TestResults = new HashSet<TestResultViewModel>();
        }

        public ICollection<ExistingTestViewModel> ExistingTests { get; set; }

        public ICollection<TestResultViewModel> TestResults { get; set; }
    }
}
