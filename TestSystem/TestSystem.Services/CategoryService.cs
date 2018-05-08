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

        public CategoryService(IEfGenericRepository<Category> categoryRepo, IMappingProvider mapper, ISaver saver, IRandomProvider random)
            : base(mapper, saver, random)
        {
            Guard.WhenArgument(categoryRepo, "categoryRepo").IsNull().Throw();
            this.categoryRepo = categoryRepo;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var entities = this.categoryRepo.All.OrderBy(c => c.Name);
            var categories = this.Mapper.ProjectTo<CategoryDto>(entities);
            return categories;
        }

        public IEnumerable<CategoryDto> GetAllWithPublsihedAndActiveTests()
        {
            var entities = this.categoryRepo.All.Include(c => c.Tests).Where(c => c.Tests.Any<Test>(t => t.IsPusblished && (t.IsDisabled ==false)));            
            var categories = this.Mapper.ProjectTo<CategoryDto>(entities);
            return categories;
        }

        public IEnumerable<CategoryDto> GetAllWithTests()
        {
            var entities = this.categoryRepo.All.Include(x => x.Tests).OrderBy(c => c.Name);
            var categories = this.Mapper.ProjectTo<CategoryDto>(entities);
            return categories.ToList();
        }
    }
}
