using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FinalProject_Dips2.Models;
using FinalProject_Dips2.services;
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

namespace FinalProject_Dips2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
       

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddScoped<IDataService<Image>, DataService<Image>>();
             services.AddScoped<IDataService<Category>, DataService<Category>>();
          
            services.AddIdentity<IdentityUser, IdentityRole>
         (
             config =>
             {
                 config.User.RequireUniqueEmail = true;
                 config.Password.RequireDigit = true;
                 config.Password.RequiredLength = 6;
                 config.Password.RequireLowercase = true;
                 config.Password.RequireNonAlphanumeric = true;
                 config.Password.RequireUppercase = true;
             }
         ).AddEntityFrameworkStores<LoginsDbContext>()
            .AddDefaultTokenProviders();
            services.AddDbContext<LoginsDbContext>();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Events.OnRedirectToAccessDenied = ReplaceRedirector(HttpStatusCode.Forbidden, options.Events.OnRedirectToAccessDenied);
                    options.Events.OnRedirectToLogin = ReplaceRedirector(HttpStatusCode.Unauthorized, options.Events.OnRedirectToLogin);
                });

                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

              app.UseAuthentication();   
              app.UseStaticFiles();
              
                
              app.UseMvcWithDefaultRoute();
  
          //  SeedHelper.Seed(app.ApplicationServices).Wait();
        }
         static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(HttpStatusCode statusCode,
            Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) =>
            context =>
            {
                if (context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = (int)statusCode;
                    return Task.CompletedTask;
                }
                return existingRedirector(context);
            };
    }
}
