using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Models.DashboardViewModels;
using TestSystem.Web.Models.Shared;

namespace TestSystem.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ICategoryService categoryService;
        private readonly ITestService testService;
        private readonly IMappingProvider mapper;
        private readonly IUserService userService;

        public DashboardController(UserManager<User> userManager, ICategoryService categoryService, ITestService testService, IMappingProvider mapper, IUserService userService)
        {
            this.userManager = userManager;
            this.categoryService = categoryService;
            this.testService = testService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var categoriesDto = this.categoryService.GetAllWithPublsihedTests();
            var userId = userManager.GetUserId(HttpContext.User);
            var testsSumbitted = testService.GetUserTests(userId);
            var tests = new List<TestViewModel>();

            foreach (var c in categoriesDto)
            {
                if (c.Tests.Count() == 0)
                {
                    continue;
                }

                var tvm = new TestViewModel() { Name = c.Name + " Test", CategoryName = c.Name };

                if (testsSumbitted.Any(x => x.Category.Name == c.Name))
                {
                    tvm.IsSubmitted = true;
                }

                tests.Add(tvm);
            }

            var model = new IndexViewModel()
            {
                Title = "Dashboard",
                Categories = (this.mapper.ProjectTo<CategoryViewModel>(categoriesDto.AsQueryable()).OrderBy(x => x.Name).ToList()),
                Tests = tests
            };

            return View("Index", model);

        }
    }
}