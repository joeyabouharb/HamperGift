using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Project_Infastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_UI.ViewModels
{
	public enum Filters { Ascending, Descending}
    public class HomeIndexViewModel
    {
       public IEnumerable<Hamper> Hampers { get; set; }
    
		public IEnumerable<SelectListItem> CategoryNames { get; set; }
	   public string categoryid { get; set; }

	   public string filter { get; set; }

	   public string q { get; set; }

		public int Total { get; set; }
        
    }
}
