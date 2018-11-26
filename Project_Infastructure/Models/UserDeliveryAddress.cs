using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Infastructure.Models
{
	public enum AddressEnum { NSW, VIC, QLD, ACT, SA, WA, NT, TAS }


	public class UserDeliveryAddress
	{
		public int UserDeliveryAddressId { get; set; }

		

		[ForeignKey("AspNetUsers")]
		public Guid ApplicationUserId { get; set; }

		public string DeliveryAddress { get; set; }

		public AddressEnum StateAddress { get; set; }

		public string PostalAddress { get; set; }


		public ICollection<CartInvoice> CartInvoices { get; set; }

	}
}
