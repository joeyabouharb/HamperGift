using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject_Dips2.Models;
using FinalProject_Dips2.ViewModels;
using FinalProject_Dips2.services;
using System.IO;

namespace FinalProject_Dips2.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
        private IDataService<Image> _imageDataService;

    public HomeController(IDataService<Hamper> HamperDataService, IDataService<Image> ImageDataService)
        {
            _hamperDataService = HamperDataService;
            _imageDataService = ImageDataService;
        } 

        [HttpGet]
        public IActionResult Index(HomeIndexViewModel vm)
        {
            IEnumerable<Hamper> hampers = _hamperDataService.GetAll();
            
            vm = new HomeIndexViewModel
            {
             Hampers = hampers
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public FileStreamResult ViewImage(int id)
        {
            Models.Image image = _imageDataService.GetSingle(im => im.ImageId == id);

            MemoryStream stream = new MemoryStream(image.Data);
            return new FileStreamResult(stream, image.ContentType);
        }


    }
}