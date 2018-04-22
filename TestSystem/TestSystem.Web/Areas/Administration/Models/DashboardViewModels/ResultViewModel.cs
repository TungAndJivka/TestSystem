using System;

namespace TestSystem.Web.Areas.Administration.Models.DashboardViewModels
{
    public class ResultViewModel
    {
        public string User { get; set; }

        public string TestName { get; set; }

        public string CategoryName { get; set; }

        public double? Score { get; set; }
        public string Result
        {
            get
            {
                if (this.Score >= 0.8)
                {
                    return "Passed";
                }
                else
                {
                    return "Failed";
                }
            }
        }

        public DateTime? StartTime { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string ExecutionTime
        {
            get
            {
                if (this.StartTime != null && this.SubmittedOn != null)
                {

                    var duration = (this.SubmittedOn = this.StartTime);
                    var result = $"{duration:%m}";
                    return result;
                }
                else
                {
                    return "";
                }
            }
        }

        public TimeSpan? Duration { get; set; }
        public string RequestedTime
        {
            get
            {
                if (this.Duration != null)
                {
                    var result = $"{this.Duration:m} min";
                    return result;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
