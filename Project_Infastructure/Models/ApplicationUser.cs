using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Project_Infastructure.Models
{
   
    public class ApplicationUser : IdentityUser<Guid>
    { 
  
       
	    public ICollection<UserDeliveryAddress> UserDeliveryAddresses { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
