using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject_Dips2.Models;
namespace FinalProject_Dips2.services
{
    public class HamperDbContext : DbContext
    {
      
        DbSet<Category> TblCategories { get; set; }

        DbSet<Product> TblProduct { get; set; }

        DbSet<Hamper> TblHamper { get; set; }

        DbSet<HamperProduct> TblHamperProducts { get; set; }
         
      
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ; Database= HamperGiftDb ; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<HamperProduct>().HasKey(sc => new { sc.HamperId, sc.ProductId });
           
        }

       
    }
}
