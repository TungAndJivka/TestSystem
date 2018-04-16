using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services;
using TestSystem.Services.Contracts;
using TestSystem.Web.Data;
using TestSystem.Web.Services;

namespace TestSystem.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Configuration = configuration;
            this.Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Data
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
                //options.UseInMemoryDatabase(); // for testing  /Effort/
            });

            services.AddScoped(typeof(IEfGenericRepository<>), typeof(EfGenericRepository<>));
            services.AddScoped<ISaver, Saver>();


            // Authentication
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            if (this.Environment.IsDevelopment())
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = 999;
                });
            }


            // Services
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();



            // Infrastructure
            services.AddMvc();
            services.AddAutoMapper();
            services.AddScoped<IMappingProvider, MappingProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "adminArea",
                   template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
