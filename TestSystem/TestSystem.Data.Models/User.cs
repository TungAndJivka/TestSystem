using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser, IDeletable
    {
        public bool IsDeleted { get; set; }

        public ICollection<UserTest> UserTests { get; set; } //NP

        public DateTime? DeletedOn { get; set; }
    }
}
