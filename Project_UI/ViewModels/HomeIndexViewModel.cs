using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Project_Infastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project_UI.ViewModels
{
	public enum Filters { Ascending, Descending}
    public class HomeIndexViewModel
    {
       public IEnumerable<Hamper> Hampers { get; set; }
    
		public IEnumerable<SelectListItem> CategoryNames { get; set; }
	   public string categoryid { get; set; }

	   public string filter { get; set; }

		[Required(ErrorMessage = "Please Enter a Quantity"),
	   Display(Name = "Quantity "),
			Range(1, 10)]
		public int Quantity { get; set; }

		public string Query { get; set; }

		public int Total { get; set; }
        
    }
}
