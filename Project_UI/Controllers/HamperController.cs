using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Project_UI.ViewModels;

using System.IO;
using Microsoft.AspNetCore.Authorization;
using Project_Infastructure.services;
using Project_Infastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_UI.Controllers
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
            Hamper hamper = _hamperDataService.GetAll()
                .Include(ha => ha.Feedbacks).
                SingleOrDefault(h => h.HamperId == id);

			HamperDetailsViewModel vm = new HamperDetailsViewModel();
			if(hamper == null)
			{
				return NotFound();
			}
			if (hamper.isDiscontinued == true)
			{
				return View(vm);
			}

            var products = _hamperProductService.GetAll().
                                                 Where(hps => hps.HamperId == id).
                                                 Include(hp => hp.Product).
                                                 Select(pr => pr.Product);

            IEnumerable<Feedback> feedbacks = hamper.Feedbacks;

         
            
            

             vm = new HamperDetailsViewModel{
                Name = hamper.HamperName,
                Cost = hamper.Cost,
                ImageId = hamper.ImageId,
                Category = _catService.GetSingle
                (cat => cat.CategoryId == hamper.CategoryId).CategoryName,
               Products = products,
               Feedbacks = feedbacks
           };
            return View(vm);
            
        }
        
        [HttpGet]
        public FileStreamResult ViewImage(int id){
            Image image = _imageDataService.GetSingle(img => img.ImageId == id);
             
              MemoryStream ms = new MemoryStream(image.Data);
              return new FileStreamResult(ms, image.ContentType);
        }
    }
}