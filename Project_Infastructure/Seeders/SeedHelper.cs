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

            var context = serviceProvider.GetRequiredService<DesignDbContext>();

            context.Database.EnsureCreated();
            if (!context.TblCategory.Any())
            {
				List<Category> cats = new List<Category> {
				 new Category {CategoryName = "Baby Boys" },
                new Category {CategoryName = "Baby Girls" },
                new Category { CategoryName = "Mens" },
                new Category { CategoryName = "Womens" },
                new Category { CategoryName = "Other" },
                new Category { CategoryName = "Kids" }
				};
               
			    await context.TblCategory.AddRangeAsync(cats);
                await context.SaveChangesAsync();
            }
            if(!context.TblProduct.Any())
            {
				List<Product> products = new List<Product>{
				new Product { ProductName = "Baby Powder", Quantity = 800, ProductSizeType = productSize.g },
				new Product { ProductName = "Toy Rattle", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "Scented nappy bags", Quantity = 480, ProductSizeType = productSize.pack },
				new Product { ProductName = "Unscented baby wipes", Quantity = 200, ProductSizeType = productSize.pack },
				new Product { ProductName = "Ultra dry jumbo nappy for newborn", Quantity = 100, ProductSizeType = productSize.pack },
				new Product { ProductName = "Baby formula", Quantity = 800, ProductSizeType = productSize.g },
				new Product { ProductName = "Baby gentle wash & shampoo", Quantity = 500, ProductSizeType = productSize.ml },
				new Product { ProductName = "Feeding bottle twin pack", Quantity = 260, ProductSizeType = productSize.ml },
				new Product { ProductName = "Anti-rash baby powder", Quantity = 100, ProductSizeType = productSize.g },
				new Product { ProductName = "Pudding", Quantity = 260, ProductSizeType = productSize.g },
				new Product { ProductName = "Christmas Ham", Quantity = 1000, ProductSizeType = productSize.g },
				new Product { ProductName = "Moscato", Quantity = 860, ProductSizeType = productSize.ml },
				new Product { ProductName = "Disney themed mug", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "Coloring book", Quantity = 3, ProductSizeType = productSize.pack },
				new Product { ProductName = "Disney Classics DVD collection", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "Frozen Singalong Disk", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "Perfume for women", Quantity = 260, ProductSizeType = productSize.ml },
				new Product { ProductName = "Wash Towel", Quantity = 2, ProductSizeType = productSize.pack },
				new Product { ProductName = "Cadles", Quantity = 2, ProductSizeType = productSize.pack },
				new Product { ProductName = "Face Towel", Quantity = 2, ProductSizeType = productSize.pack },
				new Product { ProductName = "Moisturizer", Quantity = 360, ProductSizeType = productSize.ml },

				new Product { ProductName = "beach towel", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "cheescake", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "Beer", Quantity = 12, ProductSizeType = productSize.pack },
				new Product { ProductName = "50 dollar gift card", Quantity = 1, ProductSizeType = productSize.pack },

				new Product { ProductName = "Apples", Quantity = 5, ProductSizeType = productSize.pack },
				new Product { ProductName = "Strawberry", Quantity = 460, ProductSizeType = productSize.g },
				new Product { ProductName = "Blueberry", Quantity = 200, ProductSizeType = productSize.g },
				new Product { ProductName = "peaches", Quantity = 560, ProductSizeType = productSize.g },
				new Product { ProductName = "mineral water", Quantity = 380, ProductSizeType = productSize.ml },

				new Product { ProductName = "reeces pieces", Quantity = 20, ProductSizeType = productSize.pack },
				new Product { ProductName = "Wagon wheels", Quantity = 20, ProductSizeType = productSize.pack },
				new Product { ProductName = "Harribos sweets", Quantity = 20, ProductSizeType = productSize.pack },
				new Product { ProductName = "Hersheys Chocolate", Quantity = 20, ProductSizeType = productSize.pack },

				new Product { ProductName = "Toblerone", Quantity = 500, ProductSizeType = productSize.g },
				new Product { ProductName = "Snickers", Quantity = 20, ProductSizeType = productSize.pack },
				new Product { ProductName = "Cadbury Hazelnut Chocolate", Quantity = 560, ProductSizeType = productSize.g },


				new Product { ProductName = "Perfume for men", Quantity = 500, ProductSizeType = productSize.ml },
				new Product { ProductName = "Axe Bodyspray", Quantity = 200, ProductSizeType = productSize.ml },
				new Product { ProductName = "Gym Towel", Quantity = 500, ProductSizeType = productSize.ml },
				new Product { ProductName = "Moisturizer for men", Quantity = 360, ProductSizeType = productSize.ml },
				new Product { ProductName = "Wash towel for men", Quantity = 2, ProductSizeType = productSize.pack },
				new Product { ProductName = "Argan Oil Shampoo", Quantity = 260, ProductSizeType = productSize.ml },

				new Product { ProductName = "Jatz crackers", Quantity = 480, ProductSizeType = productSize.g },
				new Product { ProductName = "Bleu cheese", Quantity = 200, ProductSizeType = productSize.g},
				new Product { ProductName = "Red Wine", Quantity = 800, ProductSizeType = productSize.ml },
				new Product { ProductName = "Dark Chocolate", Quantity = 500, ProductSizeType = productSize.g },
				new Product { ProductName = "Cheese", Quantity = 360, ProductSizeType = productSize.g },

				new Product { ProductName = "Baby PJs", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "Baby Shoes", Quantity = 1, ProductSizeType = productSize.pack },
				new Product { ProductName = "Baby Socks", Quantity = 1, ProductSizeType = productSize.pack }
				};

				await context.TblProduct.AddRangeAsync(products);
			    await context.SaveChangesAsync();
            }
            if (!context.TblImage.Any())
            {
				System.Drawing.Image image = System.Drawing.Image.FromFile(
				"C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\babyhamper.jpg");
				
				
				MemoryStream ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                image.Tag = "babyhamper";
                byte[] img = ms.ToArray();

                await context.TblImage.AddAsync(new Image {FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\baby_clothes_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyclothes";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\baby_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyHamper2";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\babyboy_hamper.png");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyHamperboy";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/png", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\christmas_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "christmas";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\disney_kids.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "disney_kids";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\hamper_for_her.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "hamper-for-her";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\happy_birthday1.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "birthdayHamper";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\Healthy_fruits.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "FruitHamper";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\Kids_Sweets.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "Sweets";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\Men_Chocolates.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "chocolatesformen";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\male_hamper1.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "male-hamper-1";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

				image = System.Drawing.Image.FromFile("C:\\Users\\joseph\\source\\repos\\FinalProject_Hamper\\Project_UI\\wwwroot\\static\\img\\Special_Wine_Cheese.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "wine and cheese";
				img = ms.ToArray();
				await context.TblImage.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

		
				await context.SaveChangesAsync();
            }
            if (!context.TblHamper.Any())
            {
				List<Hamper> hampers = new List<Hamper> {

				new Hamper {HamperName = "Newborn Baby Hamper (girl)", CategoryId = 2, ImageId = 1, Cost = 75.00M , isDiscontinued = false},
				new Hamper { HamperName = "Newborn Baby Hamper (boy)", CategoryId = 2, ImageId = 3, Cost = 85.00M, isDiscontinued = false },
				new Hamper { HamperName = "Newborn Baby Hamper ValuePack", CategoryId = 2, ImageId = 4, Cost = 30.00M, isDiscontinued = false },
				new Hamper { HamperName = "Newborn Baby Clothes", CategoryId = 2, ImageId = 2, Cost = 30.00M, isDiscontinued = false },
				new Hamper { HamperName = "Chiristmas Hamper", CategoryId = 5, ImageId = 5, Cost = 145.00M, isDiscontinued = false },
				new Hamper { HamperName = "disney hamper", CategoryId = 6, ImageId = 6, Cost = 60.00M, isDiscontinued = false },
				new Hamper { HamperName = "Hampers for her", CategoryId = 4, ImageId = 7, Cost = 85.00M, isDiscontinued = false },
				new Hamper { HamperName = "Birthday hamper", CategoryId = 6, ImageId = 8, Cost = 115.00M, isDiscontinued = false },
				new Hamper { HamperName = "Fruit hamper", CategoryId = 5, ImageId = 9, Cost = 20.00M, isDiscontinued = false },
				new Hamper { HamperName = "Sweets for kids", CategoryId = 6, ImageId = 10, Cost = 25.00M, isDiscontinued = false },
				new Hamper { HamperName = "Mens Chocolates", CategoryId = 3, ImageId = 11, Cost = 45.00M, isDiscontinued = false },
				new Hamper { HamperName = "Mens Deluxe Hamper", CategoryId = 3, ImageId = 12, Cost = 95.00M, isDiscontinued = false },
				new Hamper { HamperName = "Special Wine and Snacks hamper", CategoryId = 5, ImageId = 13, Cost = 105.00M, isDiscontinued = false }


				};

				
			
				await context.TblHamper.AddRangeAsync(hampers);
				await context.SaveChangesAsync();
            }

			if (!context.TblHamperProduct.Any())
			{
				List<HamperProduct> hps = new List<HamperProduct> {
				new HamperProduct { HamperId = 1, ProductId = 1 },
			   new HamperProduct { HamperId = 1, ProductId = 2 },
			   new HamperProduct { HamperId = 1, ProductId = 3 },
			   new HamperProduct { HamperId = 1, ProductId = 4 },
			   new HamperProduct { HamperId = 1, ProductId = 5 },
			   new HamperProduct { HamperId = 1, ProductId = 6 },
			   new HamperProduct { HamperId = 1, ProductId = 7 },
			   new HamperProduct { HamperId = 1, ProductId = 8 },
			   new HamperProduct { HamperId = 1, ProductId = 9 },

			   new HamperProduct { HamperId = 2, ProductId = 1 },
			   new HamperProduct { HamperId = 2, ProductId = 2 },
			   new HamperProduct { HamperId = 2, ProductId = 3 },
			   new HamperProduct { HamperId = 2, ProductId = 4 },
			   new HamperProduct { HamperId = 2, ProductId = 5 },
			   new HamperProduct { HamperId = 2, ProductId = 6 },
			   new HamperProduct { HamperId = 2, ProductId = 7 },
			   new HamperProduct { HamperId = 3, ProductId = 1 },
			   new HamperProduct { HamperId = 3, ProductId = 2 },
			   new HamperProduct { HamperId = 3, ProductId = 3 },
			   new HamperProduct { HamperId = 3, ProductId = 4 },

			   new HamperProduct { HamperId = 5, ProductId = 10 },
			   new HamperProduct { HamperId = 5, ProductId = 11 },
			   new HamperProduct { HamperId = 5, ProductId = 12 },

			   new HamperProduct { HamperId = 6, ProductId = 13 },
			   new HamperProduct { HamperId = 6, ProductId = 14 },
			   new HamperProduct { HamperId = 6, ProductId = 15 },
			   new HamperProduct { HamperId = 6, ProductId = 16 },


			   new HamperProduct { HamperId = 7, ProductId = 17 },
			   new HamperProduct { HamperId = 7, ProductId = 18 },
			   new HamperProduct { HamperId = 7, ProductId = 19 },
			   new HamperProduct { HamperId = 7, ProductId = 20 },
			   new HamperProduct { HamperId = 7, ProductId = 21 },

			   new HamperProduct { HamperId = 8, ProductId = 22 },
			   new HamperProduct { HamperId = 8, ProductId = 23 },
			   new HamperProduct { HamperId = 8, ProductId = 24 },
			   new HamperProduct { HamperId = 8, ProductId = 25 },

			   new HamperProduct { HamperId = 9, ProductId = 26 },
			   new HamperProduct { HamperId = 9, ProductId = 27 },
			   new HamperProduct { HamperId = 9, ProductId = 28 },
			   new HamperProduct { HamperId = 9, ProductId = 29 },
			   new HamperProduct { HamperId = 9, ProductId = 30 },
			   new HamperProduct { HamperId = 10, ProductId = 31 },
			   new HamperProduct { HamperId = 10, ProductId = 32 },
			   new HamperProduct { HamperId = 10, ProductId = 33 },
			   new HamperProduct { HamperId = 10, ProductId = 34 },

			   new HamperProduct { HamperId = 11, ProductId = 34 },
			   new HamperProduct { HamperId = 11, ProductId = 35 },
			   new HamperProduct { HamperId = 11, ProductId = 36 },
			   new HamperProduct { HamperId = 11, ProductId = 37 },

			   new HamperProduct { HamperId = 12, ProductId = 38 },
			   new HamperProduct { HamperId = 12, ProductId = 39 },
			   new HamperProduct { HamperId = 12, ProductId = 40 },
			   new HamperProduct { HamperId = 12, ProductId = 41 },
			   new HamperProduct { HamperId = 12, ProductId = 42 },
			   new HamperProduct { HamperId = 12, ProductId = 43 },

			   new HamperProduct { HamperId = 13, ProductId = 44 },
			   new HamperProduct { HamperId = 13, ProductId = 45 },
			   new HamperProduct { HamperId = 13, ProductId = 46 },
			   new HamperProduct { HamperId = 13, ProductId = 47 },
			   new HamperProduct { HamperId = 13, ProductId = 48 },

			   new HamperProduct { HamperId = 4, ProductId = 2 },
			   new HamperProduct { HamperId = 4, ProductId = 49 },
			   new HamperProduct { HamperId = 4, ProductId = 50 },
			   new HamperProduct { HamperId = 4, ProductId = 51 }
			   };

				await context.AddRangeAsync(hps);

				await context.SaveChangesAsync();
            }
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
