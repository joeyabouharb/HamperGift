using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Infastructure.Models;
namespace Project_UI.ViewModels
{
	public class UserFeedbackViewModel
	{
		public ratings rating { get; set; }
		public string comment { get; set; }

		public IEnumerable<SelectListItem> hampers { get; set; }

		public string HamperId { get; set;  }
	}
}
