using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Data.Models;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Models.TakeTestViewModels;

namespace TestSystem.Web.Controllers
{
    public class TakeTestController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITestService testService;
        private readonly IMappingProvider mapper;

        public TakeTestController(UserManager<User> userManager, ITestService testService, IMappingProvider mapper)
        {
            this.userManager = userManager;
            this.testService = testService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var testDto = this.testService.GetRandomTestByCategory(id);
            var questions = mapper.ProjectTo<QuestionViewModel>(testDto.Questions.AsQueryable()).ToList();
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            DateTime startedOn = DateTime.Now;

            var model = new IndexViewModel()
            {
                UserId = userId,
                TestId = testDto.Id,
                TestName = testDto.TestName,
                Duration = testDto.Duration,
                CategoryName = id,
                Questions = questions,
                StartedOn = startedOn
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel test)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}