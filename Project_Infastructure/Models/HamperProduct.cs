﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_Infastructure.Models
{
    public class HamperProduct
    {
       
        [ForeignKey("TblHamper")]
        public int HamperId { get; set; }
		public Hamper hamper { get; set; }
        [ForeignKey("TblProduct")]
        public int ProductId { get; set; }
		public Product Product { get; set; }

    
    }
}
