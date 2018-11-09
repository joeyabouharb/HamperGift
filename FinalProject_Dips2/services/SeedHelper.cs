using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinalProject_Dips2.Models;
namespace FinalProject_Dips2.services
{
    public class SeedHelper
    {
        public static async Task Seed(IServiceProvider provider)
        {
            //set up the scope of our services that used 
            //our DI container
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                
                //......sample data......
                //add Admin role
                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole("Admin"));
                }
                //add default admin
                if (await userManager.FindByNameAsync("admin1") == null)
                {
                    ApplicationUser admin = new ApplicationUser("admin1");
                    admin.Email = "admin1@yahoo.com";
                    await userManager.CreateAsync(admin, "Joe-a1995");//add user to Users tabel
                    await userManager.AddToRoleAsync(admin, "Admin"); //add admin1 to role Admin
                }
                //add Customer role
                if (await roleManager.FindByNameAsync("Customer") == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole("Customer"));
                }
                //add default customer
                if (await userManager.FindByNameAsync("customer1") == null)
                {
                    ApplicationUser cust = new ApplicationUser("customer1");
                    cust.Email = "customer1@yahoo.com";
                    await userManager.CreateAsync(cust, "Doe#1985");//add user to Users tabel
                    await userManager.AddToRoleAsync(cust, "Customer"); //add customer1 to role Customer
                }

            }
        }
    }
}
