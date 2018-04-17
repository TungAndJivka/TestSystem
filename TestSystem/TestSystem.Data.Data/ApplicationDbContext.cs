using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSystem.Data.Models;

namespace TestSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Seed();

        }

        private void Seed()
        {
            if (true)//this.Categories.Count() == 0)
            {
                this.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "Java" });
                this.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "SQL" });
                this.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "JavaScript" });
                this.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = ".Net" });
                this.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<AnsweredQuestion> AnsweredQuestions { get; set; }

    }
}
