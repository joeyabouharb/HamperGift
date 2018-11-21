using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_Infastructure.Models
{
	public enum ratings { one, two, three, four, five };
    public class Invoice
    {
       
        public int InvoiceId { get; set; }
		
		public string SessionKey { get; set; }

		[ForeignKey("TblDeliveryAddress")]
		public int UserDeliveryAddressId { get; set; }

		 [ForeignKey("TblHamper")]
        public int HamperId { get; set; }

		public int Quantity { get; set; }

		public bool Purchased { get; set; }

		

	}
}
