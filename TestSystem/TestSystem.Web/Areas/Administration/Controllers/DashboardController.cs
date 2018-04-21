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
        //private readonly UserManager<User> userManager;
        //private readonly ICategoryService categoryService;
        //private readonly IUserService userService;

        public DashboardController(
            ITestService testService,
            IResultService resultService,
            IMappingProvider mapper)
            //UserManager<User> userManager, 
            //ICategoryService categoryService, 
            //IUserService userService)
        {
            this.testService = testService;
            this.resultService = resultService;
            this.mapper = mapper;
            //this.userManager = userManager;
            //this.categoryService = categoryService;
            //this.userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var testsDto = testService.GetAll();
            var resultsDto = resultService.GetAll();

            var tests = mapper.EnumerableProjectTo<TestViewModel>(testsDto);
            var results = mapper.EnumerableProjectTo<ResultViewModel>(resultsDto);

            var model = new IndexViewModel()
            {
                Title = "Admin Dashboard",
                Tests = tests,
                Results = results
            };

            return View(model);
        }
    }
}
