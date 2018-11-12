using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace FinalProject_Dips2.Models
{
    public enum AddressEnum { NSW, VIC, QLD, ACT, SA, WA, NT, TAS}
    public class ApplicationUser : IdentityUser<Guid>
    { 
  
       
        public string DeliveryAddress { get; set; }

        public AddressEnum StateAddress { get; set; }

        public string PostalAddress { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
