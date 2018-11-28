using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Project_Infastructure.Models;
using Project_Infastructure.services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_UI.ViewModels;

namespace Project_UI.Controllers
{

    [Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {

        private IDataService<Hamper> _hamperDataService;
        private IDataService<Image> _imageDataService;
		private IDataService<Category> _categoryService;

    public HomeController(IDataService<Hamper> HamperDataService,
		IDataService<Image> ImageDataService,
		IDataService<Category> categoryService)
        {
            _hamperDataService = HamperDataService;
            _imageDataService = ImageDataService;
			_categoryService = categoryService;
        } 
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
		

			if (User.IsInRole("Admin")){
                return RedirectToAction("Index","Admin");
            }
			IEnumerable<Hamper> hampers = _hamperDataService.Query(h => h.isDiscontinued == false);

			var cats = _categoryService.GetAll();
			if(cats == null)
			{
				return BadRequest();
			}

			IEnumerable<SelectListItem> CatList = cats.Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName });


            HomeIndexViewModel vm = new HomeIndexViewModel
            {
             Hampers = hampers,
			 CategoryNames = CatList,
			 Total = hampers.Count()
            };
            return View(vm);
        }
        
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
		
         [AllowAnonymous]
        [HttpGet]
        public IActionResult ViewImage(int id)
        {
           Image image = _imageDataService.GetSingle(im => im.ImageId == id);
			if(image != null)
			{
				MemoryStream stream = new MemoryStream(image.Data);
				
				return new FileStreamResult(stream, image.ContentType);
			}
			else
			{
				return NotFound();
			}
					
		
       
        }
         [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
		[AllowAnonymous]
		[HttpGet]
		public IActionResult About()
		{
			return View();
		}


    }
}