using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectUI.Models
{
    public class Invoice
    {
       
        public int InvoiceId { get; set; }

        [ForeignKey("AspNetUsers")]
        public Guid ApplicationUserId { get; set; }

		public string SessionId { get; set; }

        [ForeignKey("TblHamper")]
        public int HamperId { get; set; }

		public int Quantity { get; set; }

		public bool Purchased { get; set; }
    }
}
