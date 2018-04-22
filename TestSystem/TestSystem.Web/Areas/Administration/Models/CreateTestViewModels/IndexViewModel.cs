using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Web.Areas.Administration.Models.CreateTestViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }


    }
}
