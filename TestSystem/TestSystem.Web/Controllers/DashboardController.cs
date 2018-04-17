using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestSystem.DTO;
using TestSystem.Web.Models.DashboardViewModels;

namespace TestSystem.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Administration/Dashboard");
            }
            var cat1 = new CategoryDto() { Name = "Java" };
            var cat2 = new CategoryDto() { Name = "SQL" };
            var cat3 = new CategoryDto() { Name = ".NET" };
            var cat4 = new CategoryDto() { Name = "JavaScript" };

            var tests = new List<TestDto>()
                {
                    new TestDto() { Name = "test 1" },
                    new TestDto() { Name = "test 2" },
                    new TestDto() { Name = "test 3" },
                    new TestDto() { Name = "test 4" }
                };

            var categories = new List<CategoryDto>()
                {
                    cat1, cat2, cat3, cat4
                };

            var model = new IndexViewModel()
            {
                Title = "Dashboard",
                Categories = categories,
                Tests = tests
            };

            return View("Index", model);

        }
    }
}