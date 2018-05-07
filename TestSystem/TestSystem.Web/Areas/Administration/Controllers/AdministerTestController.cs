using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Areas.Administration.Models.CreateTestViewModels;

namespace TestSystem.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class AdministerTestController : Controller    
    {
        private readonly ITestService testService;
        private readonly IMappingProvider mapper;

        public AdministerTestController(ITestService testService, IMappingProvider mapper)
        {
            this.testService = testService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AdministerTestViewModel createTestViewModel, bool id)
        {

            if (createTestViewModel == null)
            {
                return View(createTestViewModel);
            }

            if (!this.ModelState.IsValid)
            {
                return View(createTestViewModel);
            }

            var createTestDto = this.mapper.MapTo<AdministerTestDto>(createTestViewModel);
            createTestDto.IsPusblished = id;

            this.testService.CreateTest(createTestDto);

            return RedirectToRoute(new
            {
                area = "Administration",
                controller = "Dashboard",
                action = "Index"
            });

        }


        [HttpGet]        
        public IActionResult EditTest(string testName, string categoryName)
        {
            if (string.IsNullOrEmpty(testName))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(categoryName))
            {
                return this.View();
            }

            var testDto = testService.GetTestForEditing(testName, categoryName);
            var testViewModel = this.mapper.MapTo<AdministerTestViewModel>(testDto);
            testDto.Category = categoryName;

            return this.View(testViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTest(AdministerTestViewModel editTestViewModel, bool id)
        {
            if (editTestViewModel.Duration == 0 
                || editTestViewModel.Category == null 
                || editTestViewModel.Category == string.Empty
                || editTestViewModel.Questions.Count() < 1
                || editTestViewModel.Questions.Any(x => x.Description == null)
                || editTestViewModel.Questions.Any(tq => tq.Description == string.Empty)
                || editTestViewModel.Questions.Any(q => q.Answers.Any(a => a.Content == null))
                || editTestViewModel.Questions.Any(z => z.Answers.Count < 2)
                || editTestViewModel.Questions.Any(z => z.Answers.Select(a => a.IsCorrect).Count() < 1)
                )
            {
                return View(editTestViewModel);
            }

            var editTestDto = this.mapper.MapTo<EditTestDto>(editTestViewModel);
            editTestDto.IsPusblished = id;
            this.testService.EditTest(editTestDto);

            return RedirectToRoute(new
            {
                area = "Administration",
                controller = "Dashboard",
                action = "Index"
            });
        }

    }
}