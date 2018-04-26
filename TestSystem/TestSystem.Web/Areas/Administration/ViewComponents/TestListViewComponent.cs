using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Areas.Administration.Models.Shared;

namespace TestSystem.Web.Areas.Administration
{
    public class TestListViewComponent : ViewComponent
    {
        private readonly IMappingProvider mapper;
        private readonly ITestService testService;

        public TestListViewComponent(IMappingProvider mapper,ITestService testService)
        {
            this.mapper = mapper;
            this.testService = testService;
        }
        public IViewComponentResult Invoke()
        {
            var tests = testService.GetAll();
            IEnumerable<TestListModel> model = this.mapper.ProjectTo<TestListModel>(tests.AsQueryable());
            return View(model);
        }
    }
}
