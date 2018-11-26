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


		protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
           
            option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB ; Database= HamperGiftDb ; Integrated Security= True");
            //option.UseSqlServer( @"Data Source=192.168.0.10,1433;Initial Catalog=HamperGiftDb; User= SA ; Password= Ja-032083");
        }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<HamperProduct>()
				.HasKey(hp => new { hp.HamperId , hp.ProductId });
			modelBuilder.Entity<HamperProduct>()
				.HasOne(hp => hp.Product)
				.WithMany(p => p.HamperProducts)
				.HasForeignKey(hp => hp.ProductId);
			modelBuilder.Entity<HamperProduct>()
				.HasOne(hp => hp.hamper)
				.WithMany(h => h.HamperProducts)
				.HasForeignKey(hp => hp.HamperId);
		}

	}
   
}