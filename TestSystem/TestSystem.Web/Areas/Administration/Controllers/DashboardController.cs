using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Areas.Administration.Models.DashboardViewModels;

namespace TestSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    public class DashboardController: Controller
    {
        private readonly ITestService testService;
        private readonly IResultService resultService;
        private readonly IMappingProvider mapper;

        public DashboardController(
            ITestService testService,
            IResultService resultService,
            IMappingProvider mapper)
        {
            this.testService = testService;
            this.resultService = resultService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }       
        //[HttpPost]
        //public IActionResult CreateTest()
        //{
        //    return View();
        //}

    }
}
