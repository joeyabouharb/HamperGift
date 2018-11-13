using FinalProject_Dips2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_Dips2.ViewModels
{
	public class UserCartViewModel
	{
	

		public IList<string> HamperName { get; set; }

		public IList<decimal> Cost { get; set; }

		public IList<int> Quantity { get; set; }

	}
}
