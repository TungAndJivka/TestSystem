using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestSystem.DTO;
using TestSystem.Web.Areas.Administration.Models.CreateTestViewModels;

namespace TestSystem.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class CreateTestController : Controller    
    {       

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
    }
}