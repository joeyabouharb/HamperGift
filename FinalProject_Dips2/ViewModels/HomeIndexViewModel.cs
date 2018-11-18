using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ProjectUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectUI.ViewModels
{
	public enum Filters { Ascending, Descending}
    public class HomeIndexViewModel
    {
       public IEnumerable<Hamper> Hampers { get; set; }
    
		public IEnumerable<SelectList> CategoryNames { get; set; }
	   public string FilterCat { get; set; }

		public Filters CostFilter { get; set; }

	   public string Query { get; set; }
        
    }
}
