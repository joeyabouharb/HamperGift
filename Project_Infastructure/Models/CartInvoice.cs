using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project_Infastructure.Models
{
	public class CartInvoice
	{
		public Guid CartInvoiceId { get; set; }

		public bool purchaseConfirmed { get; set; }

		public int UserDeliveryAddressId { get; set; }

		public ICollection<Cart> Carts { get; set; }

	}
}
