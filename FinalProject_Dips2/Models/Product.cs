using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUI.Models
{
    public enum productSize { ml, pack, g}
    public class Product
    {  
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public productSize ProductSizeType { get; set; }

        public ICollection<HamperProduct> HamperProducts { get; set; }
    }
}
