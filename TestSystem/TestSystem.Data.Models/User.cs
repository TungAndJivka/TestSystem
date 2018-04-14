using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser, IDeletable
    {
        public bool IsDeleted { get; set; }

        public ICollection<Result> Results { get; set; } //NP
    }
}
