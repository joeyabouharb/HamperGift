using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UI.ViewModels
{
	public class AdminEditHamperViewModel
	{
		public int HamperId { get; set; }
		
		[Required, DataType(DataType.Text),
		  Display(Name = "Hamper Name")]
		public string HamperName { get; set; }

		public IList<SelectListItem> FileNames { get; set; }

		[Required,
			Display(Name = "File Name")]
		public string ImageId { get; set; }

		[Required, DataType(DataType.Currency),
			Display(Name = "Cost")]
		public decimal Cost { get; set; }


		public IList<SelectListItem> CategoryNamesList { get; set; }

		[Required,
			Display(Name = "Category Name")]
		public string CategoryId { get; set; }

		public IList<ProductCheckList> ProductNamesList { get; set; }

		public bool IsDiscontinued { get; set; }
	}
}
