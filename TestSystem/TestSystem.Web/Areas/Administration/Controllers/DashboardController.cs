﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Areas.Administration.Models.CreateTestViewModels;
using TestSystem.Web.Areas.Administration.Models.DashboardViewModels;

namespace TestSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        private readonly ITestService testService;
        private readonly IResultService resultService;
        private readonly IMappingProvider mapper;
        private IMemoryCache memoryCache;

        public DashboardController(
            ITestService testService,
            IResultService resultService,
            IMappingProvider mapper,
            IMemoryCache memoryCache)
        {
            this.testService = testService;
            this.resultService = resultService;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Index(DashboardViewModel model)
        {           
            var results = resultService.GetTestResultsForDashBoard();
            var TestResultViewModels = this.mapper.EnumerableProjectTo<TestResultDto, TestResultViewModel>(results).ToList();
            model.TestResults = TestResultViewModels;



            var existingTestDtos = testService.AllTestsForDashBoard();
            var existingTests = this.mapper.EnumerableProjectTo<ExistingTestDto, ExistingTestViewModel>(existingTestDtos).ToList();
            model.ExistingTests = existingTests;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PublishTest(string testId)
        {
            if (string.IsNullOrEmpty(testId))
            {
                return this.View();
            }

            bool result = this.testService.PublishTest(testId);

            if (!result)
            {
                TempData["Error"] = "Error! Test does not match publish requirements.";
            }

            return RedirectToAction("Index", "Dashboard");
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTest(string testName, string categoryName)
        {
            if (string.IsNullOrEmpty(testName))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(categoryName))
            {
                return this.View();
            }

            this.testService.DeleteTest(testName, categoryName);


            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DisableTest(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return this.View();
            }

            this.testService.DisableTest(Id);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnableTest(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return this.View();
            }

            this.testService.EnableTest(Id);

            return RedirectToAction("Index", "Dashboard");
        }

    }
}
