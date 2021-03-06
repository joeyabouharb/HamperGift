﻿using System;
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

        private IDataService<Hamper> _hamperService;
        private IDataService<Image> _imageDataService;
		private IDataService<Category> _categoryService;

    public HomeController(IDataService<Hamper> HamperDataService,
		IDataService<Image> ImageDataService,
		IDataService<Category> categoryService)
        {
            _hamperService = HamperDataService;
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
			IEnumerable<Hamper> hampers = _hamperService.Query(h => h.isDiscontinued == false);

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
		[HttpPost]
		public IActionResult Index(int id, int q)
		{
			if (q <= 0 || id <= 0)
			{
				return RedirectToAction("Index", "Home");
			}

			if (ModelState.IsValid)
			{
				var hamper = _hamperService.GetSingle(h => h.HamperId == id);
				if (hamper == null)
				{
					return RedirectToAction("Index", "Home");
				}

				const string keyName = "cartData";
				MapCartData cartData = new MapCartData
				{
					HamperId = id,
					HamperName = hamper.HamperName,
					Cost = hamper.Cost,
					Quantity = q
				};

				List<MapCartData> cartDatas = new List<MapCartData>();
				var data = HttpContext.Session.GetString(keyName);
				if (string.IsNullOrEmpty(data))
				{
					cartDatas.Add(cartData);
					HttpContext.Session.SetString(keyName, JsonConvert.SerializeObject(cartDatas));
				}
				else
				{

					var cache = HttpContext.Session.GetString(keyName);
					cartDatas = JsonConvert.DeserializeObject<List<MapCartData>>(cache);
					cartDatas.Add(cartData);
					HttpContext.Session.SetString(keyName, JsonConvert.SerializeObject(cartDatas));
				}
				return RedirectToAction("Cart", "User");
			}

			return RedirectToAction("Index", "Home");

		}
        [AllowAnonymous]
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