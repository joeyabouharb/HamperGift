using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject_Dips2.Models;
using System.IO;

namespace FinalProject_Dips2.services
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
                await context.TblCategories.AddAsync(new Category { CategoryName = "Seasonal" });
                await context.TblCategories.AddAsync(new Category { CategoryName = "Kids" });
                await context.SaveChangesAsync();
            }
            if (!context.TblProduct.Any())
            {
                await context.TblProduct.AddAsync(new Product { ProductName = "Scented nappy bags", Quantity = 480, ProductSizeType = productSize.pack });
                await context.TblProduct.AddAsync(new Product { ProductName = "Unscented baby wipes", Quantity = 200, ProductSizeType = productSize.pack });
                await context.TblProduct.AddAsync(new Product { ProductName = "Ultra dry jumbo nappies for newborn", Quantity = 100, ProductSizeType = productSize.pack });
                await context.TblProduct.AddAsync(new Product { ProductName = "Baby formula", Quantity = 800, ProductSizeType = productSize.g });
                await context.TblProduct.AddAsync(new Product { ProductName = "Baby gentle wash & shampoo", Quantity = 500, ProductSizeType = productSize.ml });
                await context.TblProduct.AddAsync(new Product { ProductName = "Feeding bottle twin pack", Quantity = 260, ProductSizeType = productSize.ml });
                await context.TblProduct.AddAsync(new Product { ProductName = "Anti-rash baby powder", Quantity = 100, ProductSizeType = productSize.g });
                await context.SaveChangesAsync();
            }
            if (!context.TblImages.Any())
            {
				System.Drawing.Image image = System.Drawing.Image.FromFile(
				//"/home/joseph/Documents/FinalProject_Dips2/FinalProject_Dips2/wwwroot/static/img/babyhamper.jpg");
				"C:\\Users\\joeha\\Source\\Repos\\FinalProject_Dips2\\FinalProject_Dips2\\wwwroot\\static\\img\\babyhamper.jpg");
				//"C:\\Users\\student\\Source\\Repos\\FinalProject_Dips2\\FinalProject_Dips2\\wwwroot\\static\\img\\babyhamper.jpg");

				MemoryStream ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                image.Tag = "babyhamper";
                byte[] img = ms.ToArray();
                await context.TblImages.AddAsync(new Image {FileName = "Baby Hamper", ContentType = "image/jpeg", Data = img });
             
                await context.SaveChangesAsync();
            }
            if (!context.TblHamper.Any())
            {
                await context.TblHamper.AddAsync(new Hamper {HamperName = "Newborn Baby Hamper (girl)", CategoryId = 2, ImageId = 1, Cost = 75.00M } );
           
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
                await context.SaveChangesAsync();
            }
        }
    }
}
