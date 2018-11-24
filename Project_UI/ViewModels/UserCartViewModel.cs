using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Infastructure.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UI.ViewModels
{
	public class UserCartViewModel
	{
	

		public List<MapCartData> mapCartDatas { get; set; }

		public IEnumerable<SelectListItem> Addresses { get; set; }

		[Required(ErrorMessage = "Select an Email Address"), DataType(DataType.Text),
			Display(Name = "Address: ")]
		public string AddressId { get; set; }
		[Required(ErrorMessage = "Please Enter a Quantity")
		  , DataType(DataType.Text),
	   Display(Name = "Quantity "),
			Range(1, 10)]
		public int Quantity { get; set; }
	}
}
