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

            // TESTS:
            var t1 = new Test() { Id = Guid.NewGuid(), TestName = "Test 1", CreatedOn = DateTime.Now, Duration = TimeSpan.FromMinutes(20), CategoryId = c1.Id, IsPusblished = true };
            var t2 = new Test() { Id = Guid.NewGuid(), TestName = "Test 5", CreatedOn = DateTime.Now, Duration = TimeSpan.FromMinutes(30), CategoryId = c1.Id, IsPusblished = true };
            var t3 = new Test() { Id = Guid.NewGuid(), TestName = "Test 3", CreatedOn = DateTime.Now, Duration = TimeSpan.FromMinutes(20), CategoryId = c3.Id, IsPusblished = true };
            var t4 = new Test() { Id = Guid.NewGuid(), TestName = "Test 4", CreatedOn = DateTime.Now, Duration = TimeSpan.FromMinutes(20), CategoryId = c4.Id, IsPusblished = true };

            if (this.Tests != null && this.Tests.Count() == 0)
            {
                var list = new List<Test>() { t1, t2, t3, t4 };
                this.Tests.AddRange(list);
                this.SaveChanges();
            }

            // QUESTIONS:
            var q1 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's the size of the Earth?", TestId = t1.Id };
            var q2 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's day is it?", TestId = t1.Id };
            var q3 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's the size of the Moon?", TestId = t1.Id };
            var q4 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's the distance to the Sun?", TestId = t1.Id };

            var q5 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's the size of the Earth?", TestId = t2.Id };
            var q6 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's the size of the Moon?", TestId = t2.Id };
            var q7 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's day is it?", TestId = t2.Id };
            var q8 = new Question() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Description = "What's the distance to the Sun?", TestId = t2.Id };

            if (this.Questions != null && this.Questions.Count() == 0)
            {
                var list = new List<Question>() { q1, q2, q3, q4, q5, q6, q7, q8 };
                this.Questions.AddRange(list);
                this.SaveChanges();
            }

            // ANSWERS:
            var a1 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "9 730 173 sq.m.", IsCorrect = true, QuestionID = q1.Id };
            var a2 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "2 sq.m.", IsCorrect = false, QuestionID = q1.Id };
            var a3 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "-1", IsCorrect = false, QuestionID = q1.Id };

            var a4 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "Today", IsCorrect = true, QuestionID = q2.Id };
            var a5 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "Monday", IsCorrect = false, QuestionID = q2.Id };
            var a6 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "Tuesday", IsCorrect = false, QuestionID = q2.Id };

            var a7 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "Big size", IsCorrect = true, QuestionID = q3.Id };
            var a8 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "Banana", IsCorrect = false, QuestionID = q3.Id };

            var a9 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "123344598 km", IsCorrect = true, QuestionID = q4.Id };
            var a10 = new Answer() { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Content = "1", IsCorrect = false, QuestionID = q4.Id };

            if (this.Answers != null && this.Answers.Count() == 0)
            {
                var list = new List<Answer>() { a1, a2, a3, a4, a5, a6, a7, a8 ,a9, a10};
                this.Answers.AddRange(list);
                this.SaveChanges();
            }
            bool test = true;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

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
