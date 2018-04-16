using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Configuration
{
    public class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async void Seed()
        {
            if(!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

        }
    }
}
