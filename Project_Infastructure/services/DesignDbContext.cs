using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Infastructure.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project_Infastructure.services
{
    public class DesignDbContext :
	   IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		public DesignDbContext():base()
		{

		}

		public DesignDbContext(DbContextOptions<DesignDbContext> options)
	  : base(options)
		{
		}
		public DbSet<Category> TblCategory { get; set; }

        public DbSet<Product> TblProduct { get; set; }

        public DbSet<Hamper> TblHamper { get; set; }

        public DbSet<HamperProduct> TblHamperProduct { get; set; }

        public DbSet<Models.Image> TblImage { get; set; }

		public DbSet<CartInvoice> TblCartInvoice { get; set; }

		public DbSet<Cart> TblCart { get; set; }
		public DbSet<UserDeliveryAddress> TblUserDeliveryAddress { get; set; }


		
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
		
			modelBuilder.Entity<HamperProduct>()
				.HasKey(hp => new { hp.HamperId , hp.ProductId});
			modelBuilder.Entity<HamperProduct>()
				.HasOne(hp => hp.Product)
				.WithMany(p => p.HamperProducts)
				.HasForeignKey(hp => hp.ProductId);
			modelBuilder.Entity<HamperProduct>()
				.HasOne(hp => hp.hamper)
				.WithMany(h => h.HamperProducts)
				.HasForeignKey(hp => hp.HamperId);



			Category[] cats = new List<Category> {
				 new Category {CategoryId = 1, CategoryName = "Baby Boys" },
				new Category {CategoryId = 2, CategoryName = "Baby Girls" },
				new Category {CategoryId = 3, CategoryName = "Mens" },
				new Category {CategoryId = 4, CategoryName = "Womens" },
				new Category {CategoryId = 5,  CategoryName = "Other" },
				new Category {CategoryId = 6, CategoryName = "Kids" }
				}.ToArray();

			modelBuilder.Entity<Category>().HasData(cats);
			 Product[] products = new List<Product>{
				new Product {ProductId =1, ProductName = "Baby Powder", Quantity = 800, ProductSizeType = productSize.g },
				new Product {ProductId =2, ProductName = "Toy Rattle", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId =3, ProductName = "Scented nappy bags", Quantity = 480, ProductSizeType = productSize.pack },
				new Product {ProductId =4, ProductName = "Unscented baby wipes", Quantity = 200, ProductSizeType = productSize.pack },
				new Product {ProductId =5, ProductName = "Ultra dry jumbo nappy for newborn", Quantity = 100, ProductSizeType = productSize.pack },
				new Product {ProductId =6, ProductName = "Baby formula", Quantity = 800, ProductSizeType = productSize.g },
				new Product {ProductId =7, ProductName = "Baby gentle wash & shampoo", Quantity = 500, ProductSizeType = productSize.ml },
				new Product {ProductId =8, ProductName = "Feeding bottle twin pack", Quantity = 260, ProductSizeType = productSize.ml },
				new Product {ProductId =9, ProductName = "Anti-rash baby powder", Quantity = 100, ProductSizeType = productSize.g },
				new Product {ProductId =10, ProductName = "Pudding", Quantity = 260, ProductSizeType = productSize.g },
				new Product {ProductId =11, ProductName = "Christmas Ham", Quantity = 1000, ProductSizeType = productSize.g },
				new Product {ProductId =12, ProductName = "Moscato", Quantity = 860, ProductSizeType = productSize.ml },
				new Product {ProductId =13, ProductName = "Disney themed mug", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId =14, ProductName = "Coloring book", Quantity = 3, ProductSizeType = productSize.pack },
				new Product {ProductId =15, ProductName = "Disney Classics DVD collection", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId =16, ProductName = "Frozen Singalong Disk", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId =17, ProductName = "Perfume for women", Quantity = 260, ProductSizeType = productSize.ml },
				new Product {ProductId =18, ProductName = "Wash Towel", Quantity = 2, ProductSizeType = productSize.pack },
				new Product {ProductId =19, ProductName = "Cadles", Quantity = 2, ProductSizeType = productSize.pack },
				new Product {ProductId =20, ProductName = "Face Towel", Quantity = 2, ProductSizeType = productSize.pack },
				new Product {ProductId =21, ProductName = "Moisturizer", Quantity = 360, ProductSizeType = productSize.ml },

				new Product {ProductId =22, ProductName = "beach towel", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId =23, ProductName = "cheescake", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId =24, ProductName = "Beer", Quantity = 12, ProductSizeType = productSize.pack },
				new Product {ProductId =25, ProductName = "50 dollar gift card", Quantity = 1, ProductSizeType = productSize.pack },

				new Product {ProductId =26, ProductName = "Apples", Quantity = 5, ProductSizeType = productSize.pack },
				new Product {ProductId =27, ProductName = "Strawberry", Quantity = 460, ProductSizeType = productSize.g },
				new Product {ProductId =28, ProductName = "Blueberry", Quantity = 200, ProductSizeType = productSize.g },
				new Product {ProductId = 29, ProductName = "peaches", Quantity = 560, ProductSizeType = productSize.g },
				new Product {ProductId = 30, ProductName = "mineral water", Quantity = 380, ProductSizeType = productSize.ml },

				new Product {ProductId = 31, ProductName = "reeces pieces", Quantity = 20, ProductSizeType = productSize.pack },
				new Product {ProductId = 32, ProductName = "Wagon wheels", Quantity = 20, ProductSizeType = productSize.pack },
				new Product {ProductId =33, ProductName = "Harribos sweets", Quantity = 20, ProductSizeType = productSize.pack },
				new Product {ProductId =34, ProductName = "Hersheys Chocolate", Quantity = 20, ProductSizeType = productSize.pack },

				new Product {ProductId =35, ProductName = "Toblerone", Quantity = 500, ProductSizeType = productSize.g },
				new Product {ProductId =36, ProductName = "Snickers", Quantity = 20, ProductSizeType = productSize.pack },
				new Product {ProductId =37, ProductName = "Cadbury Hazelnut Chocolate", Quantity = 560, ProductSizeType = productSize.g },


				new Product {ProductId =38, ProductName = "Perfume for men", Quantity = 500, ProductSizeType = productSize.ml },
				new Product {ProductId =39, ProductName = "Axe Bodyspray", Quantity = 200, ProductSizeType = productSize.ml },
				new Product {ProductId =40, ProductName = "Gym Towel", Quantity = 500, ProductSizeType = productSize.ml },
				new Product {ProductId =41, ProductName = "Moisturizer for men", Quantity = 360, ProductSizeType = productSize.ml },
				new Product {ProductId =42, ProductName = "Wash towel for men", Quantity = 2, ProductSizeType = productSize.pack },
				new Product {ProductId = 43, ProductName = "Argan Oil Shampoo", Quantity = 260, ProductSizeType = productSize.ml },

				new Product {ProductId = 44, ProductName = "Jatz crackers", Quantity = 480, ProductSizeType = productSize.g },
				new Product {ProductId = 45, ProductName = "Bleu cheese", Quantity = 200, ProductSizeType = productSize.g},
				new Product {ProductId = 46, ProductName = "Red Wine", Quantity = 800, ProductSizeType = productSize.ml },
				new Product {ProductId =47, ProductName = "Dark Chocolate", Quantity = 500, ProductSizeType = productSize.g },
				new Product {ProductId = 48, ProductName = "Cheese", Quantity = 360, ProductSizeType = productSize.g },

				new Product {ProductId = 49, ProductName = "Baby PJs", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId = 50, ProductName = "Baby Shoes", Quantity = 1, ProductSizeType = productSize.pack },
				new Product {ProductId = 51, ProductName = "Baby Socks", Quantity = 1, ProductSizeType = productSize.pack }
				}.ToArray();
			modelBuilder.Entity<Product>().HasData(products);

			System.Drawing.Image image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\babyhamper.jpg");
				
			
				MemoryStream ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                image.Tag = "babyhamper";
                byte[] img = ms.ToArray();

			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 1, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\baby_clothes_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyclothes";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 2, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\baby_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyHamper2";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 3, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\babyboy_hamper.png");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "babyHamperboy";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 4, FileName = image.Tag.ToString(), ContentType = "image/png", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\christmas_hamper.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "christmas";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 5, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\disney_kids.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "disney_kids";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 6, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\hamper_for_her.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "hamper-for-her";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 7, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\happy_birthday1.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "birthdayHamper";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 8, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\Healthy_fruits.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "FruitHamper";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 9, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\Kids_Sweets.jpeg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "Sweets";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 10, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });
				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\Men_Chocolates.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "chocolatesformen";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 11, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\male_hamper1.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "male-hamper-1";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 12, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

				image = System.Drawing.Image.FromFile(@"..\Project_UI\wwwroot\static\img\Special_Wine_Cheese.jpg");
				ms = new MemoryStream();
				image.Save(ms, image.RawFormat);
				image.Tag = "wine and cheese";
				img = ms.ToArray();
			modelBuilder.Entity<Image>().HasData(new Image { ImageId = 13, FileName = image.Tag.ToString(), ContentType = "image/jpeg", Data = img });

			Hamper[] hampers = new List<Hamper> {

				new Hamper {HamperId = 1,HamperName = "Newborn Baby Hamper (girl)", CategoryId = 2, ImageId = 1, Cost = 75.00M , isDiscontinued = false},
				new Hamper {HamperId = 2, HamperName = "Newborn Baby Hamper (boy)", CategoryId = 2, ImageId = 3, Cost = 85.00M, isDiscontinued = false },
				new Hamper {HamperId = 3, HamperName = "Newborn Baby Hamper ValuePack", CategoryId = 2, ImageId = 4, Cost = 30.00M, isDiscontinued = false },
				new Hamper {HamperId = 4, HamperName = "Newborn Baby Clothes", CategoryId = 2, ImageId = 2, Cost = 30.00M, isDiscontinued = false },
				new Hamper {HamperId = 5, HamperName = "Chiristmas Hamper", CategoryId = 5, ImageId = 5, Cost = 145.00M, isDiscontinued = false },
				new Hamper {HamperId =  6, HamperName = "disney hamper", CategoryId = 6, ImageId = 6, Cost = 60.00M, isDiscontinued = false },
				new Hamper {HamperId =  7, HamperName = "Hampers for her", CategoryId = 4, ImageId = 7, Cost = 85.00M, isDiscontinued = false },
				new Hamper {HamperId =  8, HamperName = "Birthday hamper", CategoryId = 6, ImageId = 8, Cost = 115.00M, isDiscontinued = false },
				new Hamper {HamperId =  9, HamperName = "Fruit hamper", CategoryId = 5, ImageId = 9, Cost = 20.00M, isDiscontinued = false },
				new Hamper {HamperId =  10, HamperName = "Sweets for kids", CategoryId = 6, ImageId = 10, Cost = 25.00M, isDiscontinued = false },
				new Hamper {HamperId =  11, HamperName = "Mens Chocolates", CategoryId = 3, ImageId = 11, Cost = 45.00M, isDiscontinued = false },
				new Hamper {HamperId =  12, HamperName = "Mens Deluxe Hamper", CategoryId = 3, ImageId = 12, Cost = 95.00M, isDiscontinued = false },
				new Hamper {HamperId =  13, HamperName = "Special Wine and Snacks hamper", CategoryId = 5, ImageId = 13, Cost = 105.00M, isDiscontinued = false }
		}.ToArray();
			modelBuilder.Entity<Hamper>().HasData(hampers);
			HamperProduct[] hps = new List<HamperProduct>{
			   new HamperProduct{ HamperId = 1, ProductId = 1 },

			   new HamperProduct { HamperId = 1, ProductId = 2 },
			   new HamperProduct { HamperId = 1, ProductId = 3 },
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
			
			   

				new HamperProduct { HamperId = 4, ProductId = 51 },
		
				new HamperProduct { HamperId = 4, ProductId = 50 },
			
				new HamperProduct { HamperId = 4, ProductId = 49 },
				
				new HamperProduct { HamperId = 4, ProductId = 2 },
			
				new HamperProduct { HamperId = 13, ProductId = 48 },
			
				new HamperProduct { HamperId = 13, ProductId = 47 },
			
				new HamperProduct { HamperId = 13, ProductId = 46 },
		
				new HamperProduct { HamperId = 13, ProductId = 45 },
			
				new HamperProduct { HamperId = 13, ProductId = 44 },
			
				new HamperProduct { HamperId = 12, ProductId = 43 },
			
				new HamperProduct { HamperId = 12, ProductId = 42 },
			
				new HamperProduct { HamperId = 12, ProductId = 41 },
			
				new HamperProduct { HamperId = 12, ProductId = 40 },
			
				new HamperProduct { HamperId = 12, ProductId = 39 },
			
				new HamperProduct { HamperId = 12, ProductId = 38 },
				
				new HamperProduct { HamperId = 11, ProductId = 37 },
			
				new HamperProduct { HamperId = 11, ProductId = 36 },
				
				new HamperProduct { HamperId = 11, ProductId = 35 },
			
				new HamperProduct { HamperId = 11, ProductId = 34 },
				
				new HamperProduct { HamperId = 10, ProductId = 34 },
				
				new HamperProduct { HamperId = 10, ProductId = 33 },
			
				new HamperProduct { HamperId = 10, ProductId = 32 },
		
				new HamperProduct { HamperId = 10, ProductId = 31 },
			
				new HamperProduct { HamperId = 9, ProductId = 30 },
			
				new HamperProduct { HamperId = 9, ProductId = 29 },
			
				new HamperProduct { HamperId = 9, ProductId = 28 },
			
				new HamperProduct { HamperId = 9, ProductId = 27 },
				
				new HamperProduct { HamperId = 9, ProductId = 26 },
			
				new HamperProduct { HamperId = 8, ProductId = 25 },
			
				new HamperProduct { HamperId = 8, ProductId = 24 },
		
				new HamperProduct { HamperId = 8, ProductId = 23 },
			
				new HamperProduct { HamperId = 8, ProductId = 22 },
			
				new HamperProduct { HamperId = 7, ProductId = 21 }
			}.ToArray();

			modelBuilder.Entity<HamperProduct>().HasData(hps);
		}

	}
   
}