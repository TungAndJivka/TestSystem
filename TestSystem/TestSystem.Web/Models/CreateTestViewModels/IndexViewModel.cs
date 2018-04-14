using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSystem.DTO;

namespace TestSystem.Web.Models.CreateTestViewModels
{
    public class IndexViewModel
    {
        public ICollection<CategoryDto> Categories { get; set; }
    }
}
