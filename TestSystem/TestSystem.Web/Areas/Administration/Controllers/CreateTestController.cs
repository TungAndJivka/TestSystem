using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Web.Areas.Administration.Controllers
{
    public class CreateTestController : Controller
    {[Area("Administration")]
        public IActionResult Index()
        {
            var categories = new List<CategoryDto>()
            {
                new CategoryDto() { Name = "Java" },
                new CategoryDto() { Name = "SQL" },
                new CategoryDto() { Name = ".NET" },
                new CategoryDto() { Name = "JavaScript" }
            };

            var model = new TestSystem.Web.Areas.Administration.Models.CreateTestViewModels.IndexViewModel() { Categories = categories };

            if (User.IsInRole("admin"))
            {
                return View(model);
            }
            else
            {
                return View(model);
                //return View("Unauthorized");
            }
        }
    }
}