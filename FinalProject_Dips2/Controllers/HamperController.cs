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
    public class HamperController : Controller
    {
         private IDataService<Hamper> _hamperDataService;
        private IDataService<Image> _imageDataService;

        private IDataService<Category> _catService;

        public HamperController(IDataService<Hamper> HamperDataService,
         IDataService<Image> ImageDataService,
         IDataService<Category> CatService)
        {
            _hamperDataService = HamperDataService;
            _imageDataService = ImageDataService;
            _catService = CatService;
        } 

     [HttpGet]
        public IActionResult Details(int id){
           Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == id);
            HamperDetailsViewModel vm = new HamperDetailsViewModel{
                Name = hamper.HamperName,
                Cost = hamper.Cost,
                ImageId = hamper.ImageId,
                Category = _catService.GetSingle
                (cat => cat.CategoryId == hamper.CategoryId).CategoryName
           };
            return View(vm);
            
        }
        [HttpGet]
        public FileStreamResult ViewImage(int id){
            Models.Image image = _imageDataService.GetSingle(img => img.ImageId == id);
            byte[] imageData = image.Data;
              MemoryStream ms = new MemoryStream(image.Data);
              return new FileStreamResult(ms, image.ContentType);
        }
    }
}