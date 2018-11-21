using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Http;
using Project_Infastructure.services;
using Project_Infastructure.Models;

namespace ProjectUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            //configure Identity Db Context

            services.AddDbContext<HamperDbContext>();
            services.AddDbContext<UserDbContext>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
           
            //configire Identity options
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                
                // User settings
                options.User.RequireUniqueEmail = true;
                
            });
            //session and authorization handlers
            services.AddSession(options =>
            {
				options.Cookie.Name = "HamperSession";
                options.IdleTimeout = TimeSpan.FromMinutes(5);
			
            });

			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => false;
				options.MinimumSameSitePolicy = SameSiteMode.Lax;
			
			});

			services.AddDistributedMemoryCache();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					.AddCookie();
            
            services.AddMvc().AddSessionStateTempDataProvider();

			
            // Add application services.
            services.AddScoped<IDataService<Image>, DataService<Image>>();
			services.AddScoped<IDataService<Invoice>, DataService<Invoice>>();
			services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Product>, DataService<Product>>();
            services.AddScoped<IDataService<HamperProduct>, DataService<HamperProduct>>();
			services.AddScoped<IDataService<UserDeliveryAddress>, DataService<UserDeliveryAddress>>();
			services.AddScoped<IDataService<Feedback>, DataService<Feedback>>();


		}




 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();

          

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();
            
            app.UseMvcWithDefaultRoute();
              
  
            SeedHelper.Seed(app.ApplicationServices).Wait();
			
           
        }

  
    }
}
