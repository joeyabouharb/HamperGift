using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Infastructure.Models
{
	public class Cart
	{
		public int CartId{ get; set; }
		
		public int HamperId { get; set; }

		public Guid CartInvoiceId { get; set; }

		public int Quantity { get; set; }

		
	}
}
