using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Areas.Administration.Models.Shared;

namespace TestSystem.Web.Areas.Administration.ViewComponents
{
    public class ResultListViewComponent : ViewComponent
    {
        private readonly IMappingProvider mapper;
        private readonly IResultService resultService;

        public ResultListViewComponent(IMappingProvider mapper, IResultService resultService)
        {
            this.mapper = mapper;
            this.resultService = resultService;
        }

        public IViewComponentResult Invoke()
        {
            var results = resultService.GetAll();
            IEnumerable<TestListModel> model = this.mapper.EnumerableProjectTo<TestListModel>(results);
            return View(model);
        }
    }
}
