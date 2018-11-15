using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectUI.services;
using ProjectUI.Models;
using ProjectUI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Collections;
using Microsoft.AspNetCore.Http;

namespace ProjectUI.Controllers
{
    [Authorize(Roles ="Customer")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManagerService;
     
        private SignInManager<ApplicationUser> _signInManagerService;

		private IDataService<Hamper> _hamperService;

		private IDataService<Invoice> _invoiceService;
        public UserController(UserManager<ApplicationUser> userManager,
                               
                                 SignInManager<ApplicationUser> signinManger,
								IDataService<Invoice> invoiceService,
								IDataService<Hamper> hamperService)
        {
            _userManagerService = userManager;
            _signInManagerService = signinManger;
			_invoiceService = invoiceService;
			_hamperService = hamperService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
              
                var login = await _signInManagerService.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, lockoutOnFailure: true);
                if (login.Succeeded)
                {
                    ClaimsIdentity identity;
                    var user = await  _userManagerService.FindByNameAsync(vm.UserName);
                    var role = await _userManagerService.GetRolesAsync(user);
                      if (role.Contains("Customer")){
                        identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, vm.UserName));
                        identity.AddClaim(new Claim(ClaimTypes.Name, vm.UserName));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Customer"));
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                         new AuthenticationProperties
                         {
                             IsPersistent = vm.RememberMe,
                             ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                         });
                       
                        return RedirectToAction("Index", "Home");
                      }
                     else if(role.Contains("Admin")){
                        identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, vm.UserName));
                        identity.AddClaim(new Claim(ClaimTypes.Name, vm.UserName));
                        var principal = new ClaimsPrincipal(identity);
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                         new AuthenticationProperties
                         {
                             IsPersistent = vm.RememberMe,
                             ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                         });
                        return RedirectToAction("Index", "Admin");
                     } 
                  
                }
                if (login.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your acount is Locked out!");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Wrong User Name/Password");
                    return View();
                }
               
            }
            
            return View(vm);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
          public async Task<IActionResult> Register(UserRegisterViewModel vm)
        {
            if (ModelState.IsValid) {

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
                    IdentityResult result2 = await _userManagerService.AddToRoleAsync(user, "Customer");
                    //go to Home/Index
                    return RedirectToAction("Index", "Home");
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
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user = await _userManagerService.GetUserAsync(User);

            UserDetailsViewModel vm = new UserDetailsViewModel {
            
                UserName = user.UserName,
                Email = user.Email,
                DeliveryAddress = user.DeliveryAddress,
                PostCode = user.PostalAddress,
                State = user.StateAddress,
                PhoneNumber = user.PhoneNumber
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Details(UserDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByNameAsync(vm.UserName);

                
                user.PhoneNumber = vm.PhoneNumber;
                user.DeliveryAddress = vm.DeliveryAddress;
                user.PostalAddress = vm.PostCode;
                user.StateAddress = vm.State;
                user.Email = vm.Email;


                IdentityResult success = await _userManagerService.UpdateAsync(user);
                if(success.Succeeded)
                {
                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in success.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View(vm);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Cart()
        {
		    string applicationUser = _userManagerService.GetUserId(User);
			List<Invoice> invoiceNo = _invoiceService.Query(inv => inv.ApplicationUserId.ToString() == applicationUser).ToList();
			List<Hamper> hampers = _hamperService.Query(h =>invoiceNo.Any(i => i.HamperId == h.HamperId)).ToList();
			List<MapCartData> cartDatas = new List<MapCartData>();
			MapCartData map = new MapCartData();
			
			

			for (int i = 0; i < hampers.Count(); i++)
			{
				for (int z = 0; z < invoiceNo.Count(); z++)
					{
						
						if(hampers[i].HamperId == invoiceNo[z].HamperId && cartDatas.Count() != 0)
					{
						cartDatas[i].editCart(invoiceNo[z].Quantity, hampers[i].Cost);
					}
					else
					{
						map = new MapCartData { HamperName = hampers[i].HamperName,
												Cost = (hampers[i].Cost * invoiceNo[z].Quantity), Quantity = invoiceNo[z].Quantity};
						if (cartDatas.Contains(map))
						{
							break;
						}
						cartDatas.Add(map);
					}

					}
				}
			
			
			
			
			

			UserCartViewModel vm = new UserCartViewModel
			{
			mapCartDatas = cartDatas

			};
           
            return View(vm);
        }
		[HttpPost]
		public async Task<IActionResult> AddToCart(int id)
		{
			var user = await _userManagerService.GetUserAsync(User);
			var userid = user.Id;
			int q = 0;
			bool tryParse = int.TryParse(Request.Form["quantity"], out q);
			if(tryParse == false)
			{
				return RedirectToAction("Index", "Home");
			}
          //  var d = HttpContext.Session;
            //var s = d.Id;
			Invoice invoice = new Invoice
			{

				HamperId = id,
				ApplicationUserId = userid,
				Quantity = q
			};
			_invoiceService.Create(invoice);
			return RedirectToAction("Cart", "User");
		}
        [HttpGet]
        public IActionResult ChangePassword() {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserPasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.GetUserAsync(User);

                if (user != null)
                {
                    var check = await _userManagerService.CheckPasswordAsync(user, vm.OldPassword);

                    if (check == true)
                    {
                        var changePassword = await _userManagerService.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);

                        if (changePassword.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {

                            ModelState.AddModelError("", "Your Current Password is incorrect");
                            return View(vm);

                        }

                    }
                   

                }
                ModelState.AddModelError("", "unspecified error occured.");
                return View(vm);
            }
            return View();
        }
     
    }
}