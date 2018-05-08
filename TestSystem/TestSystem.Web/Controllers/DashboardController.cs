using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IResultService resultService;
        private readonly IMappingProvider mapper;
        private readonly IMemoryCache memoryCache;

        public DashboardController(
            UserManager<User> userManager, 
            ICategoryService categoryService,
            IResultService resultService, 
            IMappingProvider mapper, 
            IUserService userService,
            IMemoryCache memoryCache)
        {
            this.userManager = userManager;
            this.categoryService = categoryService;
            this.resultService = resultService;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            var categoriesDto = this.categoryService.GetAllWithPublsihedAndActiveTests();
            int testsTaken;    
            var userId = userManager.GetUserId(HttpContext.User);
            var testsSumbitted = resultService.GetUserResults(userId);
            var tests = new List<TestViewModel>();

            if (!memoryCache.TryGetValue("testsTaken", out testsTaken))
            {
                testsTaken = this.resultService.GetTestsTaken();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));
                memoryCache.Set("testsTaken", testsTaken, cacheEntryOptions);
            }

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
                Tests = tests,
                TestsTaken = testsTaken
            };

            return View("Index", model);

        }
    }
}