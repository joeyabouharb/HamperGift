using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Infastructure.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace Project_Infastructure.services
{
    public class SeedHelper
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
	
			UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			RoleManager<ApplicationRole> roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

			//......sample data......
			//add Admin role
			if (await roleManager.FindByNameAsync("Admin") == null)
			{
				await roleManager.CreateAsync(new ApplicationRole
				{
					Name = "Admin"
				});
			}
			//add default admin
			if (await userManager.FindByNameAsync("admin1") == null)
			{
				ApplicationUser admin = new ApplicationUser
				{
					UserName = "admin1"
				};
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
