using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_UI.ViewModels;
using Project_Infastructure.Models;
using Project_Infastructure.services;

namespace Project_UI.Controllers
{
    public class SearchController : Controller
    {

		private IDataService<Hamper> _hamperDataService;
		private IDataService<Image> _imageDataService;
		private IDataService<Category> _categoryService;

		public SearchController(IDataService<Hamper> HamperDataService,
			IDataService<Image> ImageDataService,
			IDataService<Category> categoryService)
		{
			_hamperDataService = HamperDataService;
			_imageDataService = ImageDataService;
			_categoryService = categoryService;
		}
		[HttpGet]
		public IActionResult Result(string Query, string categoryid, string filter)
		{
			IQueryable<Hamper> hamper = _hamperDataService.Query(h => h.isDiscontinued == false);
			if (Query != null)
			{
				hamper = hamper.Where(h => h.HamperName.ToLower().Contains(Query.ToLower())).Where(hmp => hmp.isDiscontinued == false);
			}
			bool isID = int.TryParse(categoryid, out int id);
			if(isID == false && categoryid != null)
			{
				return NotFound();
			}
			if(categoryid != null)
			{
				var category = _categoryService.GetSingle(c => c.CategoryId == id);
				if(category == null)
				{
					return NotFound();
				}
				hamper = hamper.Where(hmp => hmp.CategoryId == category.CategoryId);
			}
			
			if(filter == (Array.IndexOf(Enum.GetValues(Filters.Ascending.GetType()), Filters.Ascending).ToString()))
			{
				hamper = hamper.OrderBy(h => h.Cost);

			}
			else if(filter == (Array.IndexOf(Enum.GetValues(Filters.Descending.GetType()), Filters.Descending).ToString()))
			{
				hamper = hamper.OrderByDescending(h => h.Cost);
				
			}else if (filter == null)
			{
				//do nothing
			}
			else
				{
				return NotFound();
			}
			
			HomeIndexViewModel vm = new HomeIndexViewModel
			{
				Hampers = hamper.ToList(),
				Total = hamper.Count()
			};
			return View(vm);
		}



	
	}
}