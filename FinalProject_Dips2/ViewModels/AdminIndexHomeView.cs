using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUI.ViewModels
{
	public class AdminIndexHomeView
	{
		public IEnumerable<Hamper> Hampers { get; set; }

		public IEnumerable<SelectList> CategoryNames { get; set; }
		public string FilterCat { get; set; }

		public Filters CostFilter { get; set; }

		public string Query { get; set; }

		public int TotalUsers { get; set; }

	}
}
