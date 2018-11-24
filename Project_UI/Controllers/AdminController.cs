using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Infastructure.services;
using Microsoft.AspNetCore.Identity;
using Project_Infastructure.Models;
using Project_UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace Project_UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        

         private UserManager<ApplicationUser> _userManagerService;
     
        private SignInManager<ApplicationUser> _signInManagerService;

		private IDataService<Category> _categoryService;

        private IDataService<Product> _productService;

        private IDataService<Image> _imageService;

        private IDataService<Hamper> _hamperService;

        private IDataService<HamperProduct> _HPService;

        public AdminController(UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IDataService<Category> categoryService,
         IDataService<Product> productService,
         IDataService<Image> imageService,
         IDataService<Hamper> hamperService,
         IDataService<HamperProduct> HPService){
             _signInManagerService = signInManager;
             _userManagerService = userManager;
            _categoryService = categoryService;
            _productService = productService;
            _imageService = imageService;
            _hamperService = hamperService;
            _HPService = HPService;
         }
        public IActionResult Index()

        {
			var cats = _categoryService.GetAll();

			IEnumerable<SelectListItem> CatList = cats.Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName });
			IEnumerable<Hamper> hampers = _hamperService.GetAll();
			HomeIndexViewModel vm = new HomeIndexViewModel
			{
				CategoryNames = CatList,
				Hampers = hampers
			};
            return View(vm);
        }
          [HttpPost]
        public async Task<IActionResult> Logout()
        {
           
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
           [HttpGet] 
          public IActionResult Register()

        {

            return View();
        }
            [HttpPost]

         public async Task<IActionResult> Register(AdminRegisterViewModel vm)
        {
            if(ModelState.IsValid){
                   var user = await _userManagerService.FindByNameAsync(vm.UserName);

                if (user != null)
                {
                    ModelState.AddModelError("", "User Already Exists");
                    return View(vm);
                }
                user = new ApplicationUser {
                   
                    UserName = vm.UserName,
                    
                };
                
                user.Email = vm.Email;
               
                user.PhoneNumber = vm.PhoneNumber;
                


                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
                 
                if(result.Succeeded)
                {
                    IdentityResult result2 = await _userManagerService.AddToRoleAsync(user, "Admin");
                    //go to Home/Index
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    //show errors
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View(vm);
                    }
                }
            }
            return View(vm);
            }
        

        public IActionResult AddCategory(){
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(AdminCategoryAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = _categoryService.GetSingle(cat => cat.CategoryName == vm.CategoryName);
                if(search != null)
                {
                    ModelState.AddModelError("", "Category Already Exists");
                    return View(vm);
                }
                
                _categoryService.Create(new Category {CategoryName = vm.CategoryName });

                return RedirectToAction("Index", "Admin");

            }
            return View(vm);
        }

        public IActionResult AddProduct(){
            return View();
            
        }

        [HttpPost]
        public IActionResult AddProduct(AdminAddProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = _productService.GetSingle(p => p.ProductName == vm.ProductName);
                if(search != null)
                {
                    ModelState.AddModelError("", "Product already exists in DB");
                    return View(vm);
                }
                _productService.Create(new Product
                {
                    ProductName = vm.ProductName,
                    Quantity = vm.Quantity,
                    ProductSizeType = vm.ProductSizeType
                });
                return RedirectToAction("Index", "Admin");

            }
            return View(vm);
        }
        public IActionResult AddHamper(){

            var product = _productService.GetAll().Select(p => p.ProductName).ToList();
            var productChecks = product.Select(p => new ProductCheckList
            {
                ProductName = p,
                Checked = false
            }).ToList();

			var cats = _categoryService.GetAll().ToList();
			var filenames = _imageService.GetAll().ToList();
			var catSelect = cats.Select(x => new SelectListItem
			{
				Value = x.CategoryId.ToString(),
				Text = x.CategoryName
			});
			var fileSelect = filenames.Select(x => new SelectListItem
			{
				Value = x.ImageId.ToString(),
				Text = x.FileName
			});

			AdminAddHamperViewModel vm = new AdminAddHamperViewModel
			{
				ProductNamesList = productChecks.ToArray(),
				CategoryNamesList = catSelect.ToList(),
				FileNames = fileSelect.ToList() 
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult AddHamper(AdminAddHamperViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = _hamperService.GetSingle(h => h.HamperName == vm.HamperName);
                if (search != null)
                {

                    ModelState.AddModelError("", "Hamper Already Exists");
                    return View(vm);
                }
				bool isCId = int.TryParse(vm.CategoryId, out int categoryid);
				bool isIId = int.TryParse(vm.ImageId, out int imageid);

				if(!isCId && !isIId)
				{
					return NotFound();
				}
			   var getnames = vm.ProductNamesList.Where(pl => pl.Checked == true).Select(p => p.ProductName);
               var productids = _productService.Query(p => getnames.Any(g => g == p.ProductName))
                    .Select(it => it.ProductId);


                _hamperService.Create(new Hamper
                {
                    HamperName = vm.HamperName,
                    Cost = vm.Cost,
                    ImageId = imageid,
                    CategoryId = categoryid

                });
				foreach(var pid in productids)
				{
					_HPService.Create(new HamperProduct
					{
						HamperId = _hamperService.GetSingle(h => h.HamperName == vm.HamperName).HamperId,
						ProductId = pid
				});
				}
				

                    return RedirectToAction("Index", "Admin");
                }
            return View();

        }
           
        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image){
            BinaryReader binaryReader = new BinaryReader(image.OpenReadStream());
            byte[] fileData = binaryReader.ReadBytes((int)image.Length);

            var fileName = Path.GetFileName(image.FileName);
            Image img = new Image
            {
                FileName = fileName,
                ContentType = image.ContentType,
                Data = fileData,

            };
            _imageService.Create(img);

            return RedirectToAction("Index", "Admin");
        }
		[HttpGet]
		public IActionResult EditHamper(int id)
		{
			Hamper hamper = _hamperService.GetSingle(h => h.HamperId == id);
			if(hamper == null)
			{
				return View();

			}
			var product = _productService.GetAll().ToList();
			var productChecks = product.Select(p => new ProductCheckList
			{
				ProductName = p.ProductName,
				Checked = false,
				ProductId = p.ProductId
			}).ToList();

			var hp = _HPService.Query(hps => hps.HamperId == hamper.HamperId);

			productChecks.ForEach(x =>
			{
				foreach (HamperProduct hpr in hp)
				{
					if(hpr.ProductId == x.ProductId)
					{
						x.Checked = true;
					}
				}
			});
			
			var cats = _categoryService.GetAll().ToList();
			var filenames = _imageService.GetAll().ToList();
			var catSelect = cats.Select(x => new SelectListItem
			{
				Value = x.CategoryId.ToString(),
				Text = x.CategoryName
			});
			var fileSelect = filenames.Select(x => new SelectListItem
			{
				Value = x.ImageId.ToString(),
				Text = x.FileName
			});
			AdminEditHamperViewModel vm = new AdminEditHamperViewModel
			{
				CategoryId = hamper.CategoryId.ToString(),
				Cost = hamper.Cost,
				ImageId = _imageService.GetSingle(img => img.ImageId == hamper.ImageId).ImageId.ToString(),
				HamperId = hamper.HamperId,
				HamperName = hamper.HamperName,
				FileNames = fileSelect.ToList(),
				CategoryNamesList = catSelect.ToList(),
				ProductNamesList = productChecks.ToArray(),
				IsDiscontinued = hamper.isDiscontinued
				
			};

			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> EditHamper(AdminEditHamperViewModel vm)
		{
			if (ModelState.IsValid)
			{
				bool isCId = int.TryParse(vm.CategoryId, out int categoryid);
				bool isIId = int.TryParse(vm.ImageId, out int imageid);

				if (!isCId && !isIId)
				{
					return NotFound();
				}

				Hamper hamper = new Hamper
				{
					HamperName = vm.HamperName,
					ImageId = imageid,
					CategoryId = categoryid,
					Cost = vm.Cost,
					isDiscontinued = vm.IsDiscontinued

				};
				await _hamperService.Update(hamper);

				var getnames = vm.ProductNamesList.Where(pl => pl.Checked == true).Select(p => p.ProductName);
				var productids = _productService.Query(p => getnames.Any(g => g == p.ProductName))
					 .Select(it => it.ProductId);
				 await _HPService.RemoveMany(_HPService.Query(hh => hh.HamperId == vm.HamperId));

				IEnumerable<HamperProduct> hamperProducts = productids.Select(p => new HamperProduct {
					ProductId = p,
					HamperId = vm.HamperId
				});
				await _HPService.AddMany(hamperProducts);


				return RedirectToAction("Index", "Admin");
				
			}
			return View(vm);
		}

		[HttpGet]
		public IActionResult EditCategory(int categoryid)
		{
			var cat = _categoryService.GetSingle(c => c.CategoryId == categoryid);
			if(cat == null)
			{
				return NotFound();
			}
			AdminEditCategoryViewModel vm = new AdminEditCategoryViewModel {

				CategoryId = cat.CategoryId,
				CategoryName = cat.CategoryName

			};
			
			return View(vm);


		}

		[HttpPost]
		public async Task<IActionResult> EditCategory(AdminEditCategoryViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var cat = _categoryService.GetSingle(c => c.CategoryId == vm.CategoryId);

				cat.CategoryName = vm.CategoryName;

				await _categoryService.Update(cat);

				return RedirectToAction("Index", "Admin");
			}

			
			return View(vm);


		}

		[HttpGet]
		public IActionResult Hamper(int id)
		{
			Hamper hamper = _hamperService.GetSingle(h => h.HamperId == id);

			IEnumerable<HamperProduct> hamperProducts = _HPService.Query(hp => hp.HamperId == hamper.HamperId);

			IEnumerable<Product> products = _productService.Query(p => hamperProducts.Any(ids => ids.ProductId == p.ProductId));






			HamperDetailsViewModel vm = new HamperDetailsViewModel
			{
				Name = hamper.HamperName,
				Cost = hamper.Cost,
				ImageId = hamper.ImageId,
				Category = _categoryService.GetSingle
				(cat => cat.CategoryId == hamper.CategoryId).CategoryName,
				Products = products
			};
			return View(vm);
		}
    }
}