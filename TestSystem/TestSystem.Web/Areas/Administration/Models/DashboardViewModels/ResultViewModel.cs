using System;

namespace TestSystem.Web.Areas.Administration.Models.DashboardViewModels
{
    public class ResultViewModel
    {
        public string User { get; set; }

        public string Test { get; set; }

        public double? Score { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? SubmittedOn { get; set; }
    }
}
