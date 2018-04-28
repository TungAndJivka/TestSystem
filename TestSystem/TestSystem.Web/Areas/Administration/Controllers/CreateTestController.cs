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
                area = "Adminstration",
                controller = "Dashboard",
                action = "Index"
            });
        }
    }
}