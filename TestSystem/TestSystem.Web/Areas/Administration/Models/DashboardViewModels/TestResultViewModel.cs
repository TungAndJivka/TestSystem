using System;

namespace TestSystem.Web.Areas.Administration.Models.DashboardViewModels
{
    public class TestResultViewModel
    {
        public string UserId { get; set; }

        public string TestId { get; set; }

        public string UserName { get; set; }

        public string TestName { get; set; }

        public string CategoryName { get; set; }

        public TimeSpan RequestedTime { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public double Result { get; set; }

    }
}