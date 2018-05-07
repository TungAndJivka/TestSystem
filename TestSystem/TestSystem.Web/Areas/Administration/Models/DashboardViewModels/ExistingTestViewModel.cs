namespace TestSystem.Web.Areas.Administration.Models.DashboardViewModels
{
    public class ExistingTestViewModel
    {
        public string Id { get; set; }

        public string TestName { get; set; }

        public string Category { get; set; }

        public bool IsPusblished { get; set; }

        public bool IsDisabled { get; set; }
    }
}