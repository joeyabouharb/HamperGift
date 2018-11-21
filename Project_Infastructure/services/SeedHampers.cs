using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Infastructure.Models;
using System.IO;

namespace Project_Infastructure.services
{
    public class SeedHampers
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {

            var context = serviceProvider.GetRequiredService<HamperDbContext>();
            context.Database.EnsureCreated();
            if (!context.TblCategories.Any())
            {
                await context.TblCategories.AddAsync(new Category {CategoryName = "Baby Boys" });
                await context.TblCategories.AddAsync(new Category {CategoryName = "Baby Girls" });
                await context.TblCategories.AddAsync(new Category { CategoryName = "Mens" });
                await context.TblCategories.AddAsync(new Category { CategoryName = "Womens" });
                await context.TblCategories.AddAsync(new Category { CategoryName = "Other" });
                await context.TblCategories.AddAsync(new Category { CategoryName = "Kids" });
                await context.SaveChangesAsync();
            }
            if (!context.TblProduct.Any())
            {
				await context.TblProduct.AddAsync(new Product { ProductName = "Baby Powder", Quantity = 800, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Toy Rattle", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Scented nappy bags", Quantity = 480, ProductSizeType = productSize.pack });
                await context.TblProduct.AddAsync(new Product { ProductName = "Unscented baby wipes", Quantity = 200, ProductSizeType = productSize.pack });
                await context.TblProduct.AddAsync(new Product { ProductName = "Ultra dry jumbo nappies for newborn", Quantity = 100, ProductSizeType = productSize.pack });
                await context.TblProduct.AddAsync(new Product { ProductName = "Baby formula", Quantity = 800, ProductSizeType = productSize.g });
                await context.TblProduct.AddAsync(new Product { ProductName = "Baby gentle wash & shampoo", Quantity = 500, ProductSizeType = productSize.ml });
                await context.TblProduct.AddAsync(new Product { ProductName = "Feeding bottle twin pack", Quantity = 260, ProductSizeType = productSize.ml });
                await context.TblProduct.AddAsync(new Product { ProductName = "Anti-rash baby powder", Quantity = 100, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Pudding", Quantity = 260, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Christmas Ham", Quantity = 1000, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Moscato", Quantity = 860, ProductSizeType = productSize.ml });
				await context.TblProduct.AddAsync(new Product { ProductName = "Disney themed mug", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Coloring book", Quantity = 3, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Disney Classics DVD collection", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Frozen Singalong Disk", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Perfume for women", Quantity = 260, ProductSizeType = productSize.ml });
				await context.TblProduct.AddAsync(new Product { ProductName = "Wash Towel", Quantity = 2, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Cadles", Quantity = 2, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Face Towel", Quantity = 2, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Moisturizer", Quantity = 360, ProductSizeType = productSize.ml });

				await context.TblProduct.AddAsync(new Product { ProductName = "beach towel", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "cheescake", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Beer", Quantity = 12, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "50 dollar gift card", Quantity = 1, ProductSizeType = productSize.pack });

				await context.TblProduct.AddAsync(new Product { ProductName = "Apples", Quantity = 5, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Strawberries", Quantity = 460, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Blueberries", Quantity = 200, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "peaches", Quantity = 560, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "mineral water", Quantity = 380, ProductSizeType = productSize.ml });

				await context.TblProduct.AddAsync(new Product { ProductName = "reeces pieces", Quantity = 20, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Wagon wheels", Quantity = 20, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Harribos sweets", Quantity = 20, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Hersheys Chocolate", Quantity = 20, ProductSizeType = productSize.pack });

				await context.TblProduct.AddAsync(new Product { ProductName = "Toblerone", Quantity = 500, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Snickers", Quantity = 20, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Cadbury Hazelnut Chocolate", Quantity = 560, ProductSizeType = productSize.g });


				await context.TblProduct.AddAsync(new Product { ProductName = "Perfume for men", Quantity = 500, ProductSizeType = productSize.ml });
				await context.TblProduct.AddAsync(new Product { ProductName = "Axe Bodyspray", Quantity = 200, ProductSizeType = productSize.ml });
				await context.TblProduct.AddAsync(new Product { ProductName = "Gym Towel", Quantity = 500, ProductSizeType = productSize.ml });
				await context.TblProduct.AddAsync(new Product { ProductName = "Moisturizer for men", Quantity = 360, ProductSizeType = productSize.ml });
				await context.TblProduct.AddAsync(new Product { ProductName = "Wash towel for men", Quantity = 2, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Argan Oil Shampoo", Quantity = 260, ProductSizeType = productSize.ml });

				await context.TblProduct.AddAsync(new Product { ProductName = "Jatz crackers", Quantity = 480, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Bleu cheese", Quantity = 200, ProductSizeType = productSize.g});
				await context.TblProduct.AddAsync(new Product { ProductName = "Red Wine", Quantity = 800, ProductSizeType = productSize.ml });
				await context.TblProduct.AddAsync(new Product { ProductName = "Dark Chocolate", Quantity = 500, ProductSizeType = productSize.g });
				await context.TblProduct.AddAsync(new Product { ProductName = "Cheese", Quantity = 360, ProductSizeType = productSize.g });

				await context.TblProduct.AddAsync(new Product { ProductName = "Baby PJs", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Baby Shoes", Quantity = 1, ProductSizeType = productSize.pack });
				await context.TblProduct.AddAsync(new Product { ProductName = "Baby Socks", Quantity = 1, ProductSizeType = productSize.pack });





				await context.SaveChangesAsync();
            }
            if (!context.TblImages.Any())
            {
				System.Drawing.Image image = System.Drawing.Image.FromFile(
				//"/home/joseph/Documents/ProjectUI/ProjectUI/wwwroot/static/img/babyhamper.jpg");
				"C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\babyhamper.jpg");
				//"C:\\Users\\student\\Source\\Repos\\ProjectUI\\ProjectUI\\wwwroot\\static\\img\\babyhamper.jpg");
				
				MemoryStream ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                image.Tag = "babyhamper";
				
                byte[] img = ms.ToArray();
                await context.TblImages.AddAsync(new Image {FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\baby_clothes_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyclothes";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\baby_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyHamper2";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\babyboy_hamper.png");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyHamperboy";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/png", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\christmas_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "christmas";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\disney_kids.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "disney_kids";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\hamper_for_her.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "hamper-for-her";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\happy_birthday1.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "birthdayHamper";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\Healthy_fruits.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "FruitHamper";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\Kids_Sweets.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "Sweets";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\Men_Chocolates.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "chocolatesformen";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\male_hamper1.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "hamper-for-her";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

				image = System.Drawing.Image.FromFile("C:\\Users\\joeha\\source\\repos\\FinalProject_Dips2\\Project_UI\\wwwroot\\static\\img\\Special_Wine_Cheese.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "wine and cheese";
				img = ms.ToArray();
				await context.TblImages.AddAsync(new Image { FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

		
				await context.SaveChangesAsync();
            }
            if (!context.TblHamper.Any())
            {
                await context.TblHamper.AddAsync(new Hamper {HamperName = "Newborn Baby Hamper (girl)", CategoryId = 2, ImageId = 1, Cost = 75.00M , isDiscontinued = false} );
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Newborn Baby Hamper (boy)", CategoryId = 2, ImageId = 3, Cost = 85.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Newborn Baby Hamper ValuePack", CategoryId = 2, ImageId = 4, Cost = 30.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Newborn Baby Clothes", CategoryId = 2, ImageId = 2, Cost = 30.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Chiristmas Hamper", CategoryId = 5, ImageId = 5, Cost = 145.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "disney hamper", CategoryId = 6, ImageId = 6, Cost = 60.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Hampers for her", CategoryId = 4, ImageId = 7, Cost = 85.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Birthday hamper", CategoryId = 6, ImageId = 8, Cost = 115.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Fruit hamper", CategoryId = 5, ImageId = 9, Cost = 20.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Sweets for kids", CategoryId = 6, ImageId = 10, Cost = 25.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Mens Chocolates", CategoryId = 3, ImageId = 11, Cost = 45.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Mens Deluxe Hamper", CategoryId = 3, ImageId = 12, Cost = 95.00M, isDiscontinued = false });
				await context.TblHamper.AddAsync(new Hamper { HamperName = "Special Wine and Snacks hamper", CategoryId = 5, ImageId = 13, Cost = 105.00M, isDiscontinued = false });
				
			

				await context.SaveChangesAsync();
            }

            if (!context.TblHamperProducts.Any())
            {
                await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 1 });
                await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 2 });
                await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 3 });
                await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 4 });
                await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 5 });
                await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 6 });
                await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 7 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 8 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 1, ProductId = 9 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 2, ProductId = 1 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 2, ProductId = 2 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 2, ProductId = 3 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 2, ProductId = 4 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 2, ProductId = 5 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 2, ProductId = 6 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 2, ProductId = 7 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 3, ProductId = 1 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 3, ProductId = 2 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 3, ProductId = 3 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 3, ProductId = 4 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 5, ProductId = 10 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 5, ProductId = 11 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 5, ProductId = 12 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 6, ProductId = 13 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 6, ProductId = 14 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 6, ProductId = 15 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 6, ProductId = 16 });


				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 7, ProductId = 17 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 7, ProductId = 18 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 7, ProductId = 19 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 7, ProductId = 20 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 7, ProductId = 21 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 8, ProductId = 22 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 8, ProductId = 23 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 8, ProductId = 24 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 8, ProductId = 25 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 9, ProductId = 26 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 9, ProductId = 27 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 9, ProductId = 28 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 9, ProductId = 29 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 9, ProductId = 30 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 10, ProductId = 31 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 10, ProductId = 32 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 10, ProductId = 33 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 10, ProductId = 34 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 11, ProductId = 34 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 11, ProductId = 35 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 11, ProductId = 36 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 11, ProductId = 37 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 12, ProductId = 38 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 12, ProductId = 39 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 12, ProductId = 40 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 12, ProductId = 41 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 12, ProductId = 42 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 12, ProductId = 43 });


				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 13, ProductId = 44 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 13, ProductId = 45 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 13, ProductId = 46 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 13, ProductId = 47 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 13, ProductId = 48 });

				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 4, ProductId = 2 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 4, ProductId = 49 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 4, ProductId = 50 });
				await context.TblHamperProducts.AddAsync(new HamperProduct { HamperId = 4, ProductId = 51 });



				await context.SaveChangesAsync();
            }
        }
    }
}
