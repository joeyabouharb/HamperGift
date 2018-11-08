using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject_Dips2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinalProject_Dips2.services
{
    public class LoginsDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB ; Database= HamperGiftDb ; Integrated Security= True");
            //option.UseSqlServer(@"Server=localhost ; Database= HamperGiftDb ; User= SA ; Password= Ja-032083");
        } 
    }
}