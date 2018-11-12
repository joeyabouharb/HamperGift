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
        private IDataService<Product> _productDataService;
        private IDataService<Category> _catService;
        private IDataService<HamperProduct> _hamperProductService;
        public HamperController(IDataService<Hamper> HamperDataService,
         IDataService<Image> ImageDataService,
         IDataService<Category> CatService,
        IDataService<Product> productDataService,
        IDataService<HamperProduct> hamperProductDataService)
        {
            _hamperDataService = HamperDataService;
            _imageDataService = ImageDataService;
            _catService = CatService;
            _productDataService = productDataService;
            _hamperProductService = hamperProductDataService;
        } 

     [HttpGet]
        public IActionResult Details(int id){
           Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == id);

            IEnumerable<HamperProduct> hamperProducts = _hamperProductService.Query(hp => hp.HamperId == hamper.HamperId);

            IEnumerable<Product> products = _productDataService.Query(p => hamperProducts.Any(ids => ids.ProductId == p.ProductId));
         

         
            
            

            HamperDetailsViewModel vm = new HamperDetailsViewModel{
                Name = hamper.HamperName,
                Cost = hamper.Cost,
                ImageId = hamper.ImageId,
                Category = _catService.GetSingle
                (cat => cat.CategoryId == hamper.CategoryId).CategoryName,
               Products = products
           };
            return View(vm);
            
        }
        [HttpGet]
        public FileStreamResult ViewImage(int id){
            Models.Image image = _imageDataService.GetSingle(img => img.ImageId == id);
             
              MemoryStream ms = new MemoryStream(image.Data);
              return new FileStreamResult(ms, image.ContentType);
        }
    }
}