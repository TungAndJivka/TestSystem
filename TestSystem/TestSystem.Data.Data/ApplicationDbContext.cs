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
            // Testing data:
            if (this.Categories != null && this.Categories.Count() > 0)
            {
                return;
            }

            // CATEGORIES:
            var c1 = new Category() { CreatedOn = DateTime.Now, Id = Guid.NewGuid(), Name = "Java" };
            var c2 = new Category() { CreatedOn = DateTime.Now, Id = Guid.NewGuid(), Name = "SQL" };
            var c3 = new Category() { CreatedOn = DateTime.Now, Id = Guid.NewGuid(), Name = "JavaScript" };
            var c4 = new Category() { CreatedOn = DateTime.Now, Id = Guid.NewGuid(), Name = ".NET" };

            if (this.Categories != null && this.Categories.Count() == 0)
            {
                var list = new List<Category>() { c1, c2, c3, c4 };
                this.Categories.AddRange(list);
                this.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Test>()
                .HasOne<Category>(t => t.Category)
                .WithMany(c => c.Tests)
                .HasForeignKey(t => t.CategoryId);

            builder.Entity<Question>()
                .HasOne<Test>(q => q.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TestId);

            builder.Entity<Answer>()
                .HasOne<Question>(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionID);

            builder.Entity<AnsweredQuestion>()
                .HasOne(x => x.UserTest)
                .WithMany(x => x.AnsweredQuestions)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<AnsweredQuestion> AnsweredQuestions { get; set; }

    }
}
