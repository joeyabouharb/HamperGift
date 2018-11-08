using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject_Dips2.Models;
using System.IO;
using System.Drawing;

namespace FinalProject_Dips2.services
{
    public class HamperDbContext : DbContext
    {
      
        DbSet<Category> TblCategories { get; set; }

        DbSet<Product> TblProduct { get; set; }

        DbSet<Hamper> TblHamper { get; set; }

        DbSet<HamperProduct> TblHamperProducts { get; set; }
         
        DbSet<Models.Image> TblImages { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            //option.UseSqlServer(@"Server=localhost ; Database= HamperGiftDb ; User= SA ; Password= Ja-032083");
            option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB ; Database= HamperGiftDb ; Integrated Security= True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, CategoryName = "Womens" },
                new Category { CategoryId = 2, CategoryName = "Men" },
                  new Category { CategoryId = 3, CategoryName = "Children" },
                    new Category { CategoryId = 4, CategoryName = "Special Occcasions" },
                    new Category { CategoryId = 5, CategoryName = "Baby" }
            );
            System.Drawing.Image image = System.Drawing.Image.FromFile(
                 //"/home/joseph/Documents/FinalProject_Dips2/FinalProject_Dips2/wwwroot/static/img/babyhamper.jpg");
                 "C:\\Users\\student\\Source\\Repos\\FinalProject_Dips2\\FinalProject_Dips2\\wwwroot\\static\\img\\babyhamper.jpg");

             MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            image.Tag = "babyhamper";
            byte[] img = ms.ToArray();

            var i = new Models.Image
            {
                ImageId = 1,
                Data = img,
                ContentType = "image/jpeg",
                FileName = image.Tag.ToString(),
                Height = image.Height,
                Width = image.Width
            }; 

           modelBuilder.Entity<Models.Image>().HasData(i);

            modelBuilder.Entity<Hamper>().HasData(
                new Hamper
                {
                    HamperId = 1,
                    HamperName = "NewBorn Baby Hamper",
                    ImageId = 1,
                    Cost = 75.00,
                    CategoryId = 5
                });

            

        }


    }
}

