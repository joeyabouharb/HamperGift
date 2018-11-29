using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_Infastructure.Models
{
    public class HamperProduct
    {
       
        public int HamperId { get; set; }
		public Hamper hamper { get; set; }
        public int ProductId { get; set; }
		public Product Product { get; set; }

    
    }
}
