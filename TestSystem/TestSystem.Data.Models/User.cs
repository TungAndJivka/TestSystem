using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    public class User : IdentityUser, IDeletable
    {
        public User()
        {
            this.Tests = new HashSet<Test>();
            this.UserTests = new HashSet<UserTest>();
        }

        public ICollection<Test> Tests { get; set; }

        public bool IsDeleted { get; set; }


        public ICollection<UserTest> UserTests { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
