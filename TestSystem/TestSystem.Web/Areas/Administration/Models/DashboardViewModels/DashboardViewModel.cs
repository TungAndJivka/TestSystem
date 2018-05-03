using System.Collections.Generic;

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

        public bool ShowAlert { get; set; }

        public void HideAlert()
        {
            this.ShowAlert = false;
        }
    }
}
