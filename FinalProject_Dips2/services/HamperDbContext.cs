﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectUI.Models;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectUI.services
{
    public class HamperDbContext :
    DbContext
    {
        public DbSet<Category> TblCategories { get; set; }

        public DbSet<Product> TblProduct { get; set; }

        public DbSet<Hamper> TblHamper { get; set; }

        public DbSet<HamperProduct> TblHamperProducts { get; set; }
         
        public DbSet<Models.Image> TblImages { get; set; }

        public DbSet<Invoice> TblInvoices { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
			option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB ; Database= HamperGiftDb ; Integrated Security= True");
              //option.UseSqlServer( @"Data Source=192.168.0.10,1433;Initial Catalog=HamperGiftDb; User= SA ; Password= Ja-032083");
		}

    
    }
}

