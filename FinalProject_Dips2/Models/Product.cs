using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_Dips2.Models
{
    public class Product
    {  
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string productSize { get; set; }

        public ICollection<HamperProduct> HamperProducts { get; set; }
    }
}
