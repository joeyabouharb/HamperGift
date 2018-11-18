using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectUI.Models;
using ProjectUI.ViewModels;
using ProjectUI.services;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
namespace ProjectUI.Controllers
{
    [Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {

        private IDataService<Hamper> _hamperDataService;
        private IDataService<Image> _imageDataService;

    public HomeController(IDataService<Hamper> HamperDataService, IDataService<Image> ImageDataService)
        {
            _hamperDataService = HamperDataService;
            _imageDataService = ImageDataService;
        } 
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if(User.IsInRole("Admin")){
                return RedirectToAction("Index","Admin");
            }
            IEnumerable<Hamper> hampers = _hamperDataService.GetAll();
            
            HomeIndexViewModel vm = new HomeIndexViewModel
            {
             Hampers = hampers
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
        public FileStreamResult ViewImage(int id)
        {
            Models.Image image = _imageDataService.GetSingle(im => im.ImageId == id);

            MemoryStream stream = new MemoryStream(image.Data);
            return new FileStreamResult(stream, image.ContentType);
        }
         [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }



    }
}