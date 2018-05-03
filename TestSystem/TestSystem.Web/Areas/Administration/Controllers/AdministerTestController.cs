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
        public IActionResult Index(AdministerTestViewModel createTestViewModel)
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

            var createTestDto = this.mapper.MapTo<AdministerTestDto>(createTestViewModel);

            this.testService.CreateTest(createTestDto);
            //try
            //{
                
            //}
            //catch (Exception)
            //{
            //    return View(createTestViewModel);
            //}

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

            var testDto = testService.GetTest(testName, categoryName);
            var testViewModel = this.mapper.MapTo<AdministerTestViewModel>(testDto);

            return this.View(testViewModel);
        }
    }
}