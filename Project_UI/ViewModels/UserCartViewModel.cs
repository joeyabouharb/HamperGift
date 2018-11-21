using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Infastructure.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UI.ViewModels
{
	public class UserCartViewModel
	{
	

		public List<MapCartData> mapCartDatas { get; set; }

		public IEnumerable<SelectListItem> Addresses { get; set; }

		public string AddressId { get; set; }
	}
}
