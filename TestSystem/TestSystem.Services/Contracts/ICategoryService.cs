using System.Collections.Generic;
using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();

        IEnumerable<CategoryDto> GetAllWithTests();

        IEnumerable<CategoryDto> GetAllWithPublsihedAndActiveTests();
    }
}
