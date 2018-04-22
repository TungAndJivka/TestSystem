using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Areas.Administration.Models.DashboardViewModels;

namespace TestSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    public class CreateTestController : Controller
    {
        private readonly ITestService testService;
        private readonly ICategoryService categoryService;
        private readonly IMappingProvider mapper;

        public CreateTestController(
            ITestService testService,
            ICategoryService categoryService,
            IMappingProvider mapper)
        {
            this.testService = testService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new TestSystem.Web.Areas.Administration.Models.CreateTestViewModels.IndexViewModel()
            {
                Categories = this.categoryService.GetAll()
            };

            if (User.IsInRole("Admin"))
            {
                return View(model);
            }
            else
            {
                return View("Unauthorized");
            }
        }
    }
}