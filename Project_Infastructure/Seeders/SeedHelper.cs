﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Project_Infastructure.Models;
namespace Project_Infastructure.services
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
                    await roleManager.CreateAsync(new ApplicationRole {
                        Name = "Admin" });
                }
                //add default admin
                if (await userManager.FindByNameAsync("admin1") == null)
                {
                    ApplicationUser admin = new ApplicationUser{
                        UserName = "admin1" };
                    admin.Email = "admin1@yahoo.com";
                    await userManager.CreateAsync(admin, "Joe-a1995");//add user to Users tabel
                    await userManager.AddToRoleAsync(admin, "Admin"); //add admin1 to role Admin
                }
                //add Customer role
                if (await roleManager.FindByNameAsync("Customer") == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = "Customer"
                    });
                }
                //add default customer
                if (await userManager.FindByNameAsync("customer1") == null)
                {
                    ApplicationUser cust = new ApplicationUser
                    {
                        UserName = "customer1"
                    };
                    cust.Email = "customer1@yahoo.com";
                    await userManager.CreateAsync(cust, "Doe#1985");//add user to Users tabel
                    await userManager.AddToRoleAsync(cust, "Customer"); //add customer1 to role Customer
                }

            }
       
        }
     
    }
}