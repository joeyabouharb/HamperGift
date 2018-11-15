using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUI.Models
{
	public class MapCartData
	{
		public string HamperName { get; set; }

		public decimal Cost { get; set; }

		public int Quantity { get; set; }

		public void editCart(int Quantity, decimal Cost)
		{
			this.Quantity += Quantity;
			this.Cost = Cost * this.Quantity;
			
		}
	}
}
