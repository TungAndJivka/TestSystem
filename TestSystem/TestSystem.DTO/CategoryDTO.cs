
using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class CategoryDto
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<TestDto> Tests { get; set; }
    }
}
