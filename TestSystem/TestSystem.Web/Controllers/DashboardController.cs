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
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Administration/Dashboard");
            }

            var categoriesDto = this.categoryService.GetAll();
            var userId = userManager.GetUserId(HttpContext.User);
            var user = this.userService.GetUserByIdWithTests(userId);

            var model = new IndexViewModel()
            {
                Title = "Dashboard",
                Categories = (this.mapper.ProjectTo<CategoryViewModel>(categoriesDto.AsQueryable())).ToList(),
                //Tests = tests
            };

            return View("Index", model);

        }
    }
}