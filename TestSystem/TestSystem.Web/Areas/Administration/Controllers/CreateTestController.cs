using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Areas.Administration.Models.CreateTestViewModels;

namespace TestSystem.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class CreateTestController : Controller    
    {
        private readonly ITestService testService;
        private readonly IMappingProvider mapper;

        public CreateTestController(ITestService testService, IMappingProvider mapper)
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
        public IActionResult Index(CreateTestViewModel model)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTest(CreateTestViewModel createTestViewModel)
        {
            if (createTestViewModel == null)
            {
                return View(createTestViewModel);
            }
            //Vallidations

            if (!this.ModelState.IsValid)
            {
                return View(createTestViewModel);
            }

            var createTestDto = this.mapper.MapTo<TestDto>(createTestViewModel);

            try
            {
                this.testService.CreateTest(createTestDto);
            }
            catch (Exception)
            {
                return View(createTestViewModel);
            }

            return RedirectToRoute(new
            {
                area = "Admin",
                controller = "Manage",
                action = "Index"
            });
        }
    }
}