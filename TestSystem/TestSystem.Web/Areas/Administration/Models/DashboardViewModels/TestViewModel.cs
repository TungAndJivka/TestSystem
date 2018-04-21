using System;

namespace TestSystem.Web.Areas.Administration.Models.DashboardViewModels
{
    public class TestViewModel
    {
        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsPusblished { get; set; }

        public string CategoryName { get; set; }

        public string Status
        {
            get
            {
                if (this.IsPusblished)
                {
                    return "Published";
                }
                else
                {
                    return "Draft";
                }
            }
        }
    }
}
