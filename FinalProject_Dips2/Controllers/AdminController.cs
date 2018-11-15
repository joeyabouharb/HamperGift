using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectUI.services;
using Microsoft.AspNetCore.Identity;
using ProjectUI.Models;
using ProjectUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace ProjectUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        

         private UserManager<ApplicationUser> _userManagerService;
     
        private SignInManager<ApplicationUser> _signInManagerService;

		private IDataService<Category> _categoryService;

        private IDataService<Product> _productService;

        private IDataService<Models.Image> _imageService;

        private IDataService<Hamper> _hamperService;

        private IDataService<HamperProduct> _HPService;

        public AdminController(UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IDataService<Category> categoryService,
         IDataService<Product> productService,
         IDataService<Models.Image> imageService,
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
            return View();
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
                user.DeliveryAddress = vm.DeliveryAddress + " " + vm.DeliveryAddress2;
                user.StateAddress = vm.State;
                user.PostalAddress = vm.PostCode;
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
            });

            var cats = _categoryService.GetAll().Select(c => c.CategoryName).ToList();
            var filenames = _imageService.GetAll().Select(i => i.FileName).ToList();
            var catSelect = cats.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            });
            var fileSelect = filenames.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            });

            AdminAddHamperViewModel vm = new AdminAddHamperViewModel
            {
                ProductNamesList = productChecks.ToList(),
                CategoryNamesList = catSelect.ToList(),
                FileNames = fileSelect 
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
                var categoryid = _categoryService.GetSingle(c => c.CategoryName == vm.CategoryName)
                    .CategoryId;

                var imageid = _imageService.GetSingle(i => i.FileName == vm.FileName).ImageId;

                //var getnames = vm.ProductNamesList.Where(pl => pl.Checked == true).Select(p => p.ProductName);
               // var productids = _productService.Query(p => getnames.Any(g => g == p.ProductName))
                    //.Select(it => it.ProductId);


                _hamperService.Create(new Hamper
                {
                    HamperName = vm.HamperName,
                    Cost = vm.Cost,
                    ImageId = imageid,
                    CategoryId = categoryid

                }); 

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
            Models.Image img = new Image
            {
                FileName = fileName,
                ContentType = image.ContentType,
                Data = fileData,

            };
            _imageService.Create(img);

            return RedirectToAction("Index", "Admin");
        }
    }
}