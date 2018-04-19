using Bytes2you.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;

namespace TestSystem.Services
{
    public class CategoryService : AbstractService, ICategoryService
    {
        private IEfGenericRepository<Category> categoryRepo;

        public CategoryService(IEfGenericRepository<Category> categoryRepo, IMappingProvider mapper, ISaver saver)
            : base(mapper, saver)
        {
            Guard.WhenArgument(categoryRepo, "categoryRepo").IsNull().Throw();
            this.categoryRepo = categoryRepo;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var entities = this.categoryRepo.All.ToList();
            var categories = this.Mapper.ProjectTo<CategoryDto>(entities);
            return categories;
        }

        public IEnumerable<CategoryDto> GetAllWithTests()
        {
            var entities = this.categoryRepo.All.Include(x => x.Tests).ToList();
            var categories = this.Mapper.ProjectTo<CategoryDto>(entities);
            return categories;
        }
    }
}
