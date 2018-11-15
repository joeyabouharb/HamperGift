using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectUI.Models
{
    public class HamperProduct
    {
        public int HamperProductId { get; set; }
        [ForeignKey("TblHamper")]
        public int HamperId { get; set; }
        [ForeignKey("TblProduct")]
        public int ProductId { get; set; }

    
    }
}
