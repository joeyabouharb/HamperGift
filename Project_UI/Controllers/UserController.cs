using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Project_UI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Infastructure.Models;
using Project_Infastructure.services;
using Newtonsoft.Json;

namespace Project_UI.Controllers
{
    [Authorize(Roles ="Customer")]
    public class UserController : Controller
    {
		

		private UserManager<ApplicationUser> _userManagerService;
     
        private SignInManager<ApplicationUser> _signInManagerService;

		private IDataService<Hamper> _hamperService;

		private IDataService<CartInvoice> _invoiceService;

		private IDataService<Cart> _cartService;

		private IDataService<UserDeliveryAddress> _addressService;
		private IDataService<Feedback> _feedBackService;

        public UserController(UserManager<ApplicationUser> userManager,
                               
                                 SignInManager<ApplicationUser> signinManger,
								IDataService<CartInvoice> invoiceService,
								IDataService<Hamper> hamperService,
								IDataService<UserDeliveryAddress> adressService,
								IDataService<Feedback> feedbackService,
								IDataService<Cart> cartService
								)
        {
            _userManagerService = userManager;
            _signInManagerService = signinManger;
			_invoiceService = invoiceService;
			_hamperService = hamperService;
			_addressService = adressService;
			_feedBackService = feedbackService;
			_cartService = cartService;
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
                user.PhoneNumber = vm.PhoneNumber;


                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
                 
                if(result.Succeeded)
                {
                    IdentityResult result2 = await _userManagerService.AddToRoleAsync(user, "Customer");

					UserDeliveryAddress address = new UserDeliveryAddress
					{
						DeliveryAddress = vm.DeliveryAddress + " " + vm.DeliveryAddress2,
						StateAddress = vm.State,
						PostalAddress = vm.PostCode,
						ApplicationUserId = user.Id
					
					};

					await _addressService.Create(address);
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
			var addresses = _addressService.Query(a => a.ApplicationUserId == user.Id).ToList();

			var addressSelect = addresses.Select(adr => new SelectListItem
			{
				Value = adr.UserDeliveryAddressId.ToString(),
				Text = adr.DeliveryAddress

			});
			UserDetailsViewModel vm = new UserDetailsViewModel {

				Addresses = addressSelect,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber
            };

            return View(vm);
        }

		[HttpGet]
		public IActionResult UpdateAddressDetails(string AddressId)
		{
		
			var addresses = _addressService.GetSingle(a => a.UserDeliveryAddressId.ToString() == AddressId);
			if(addresses == null)
			{
				return NotFound();
			}
			
			UserAddressDetailsViewModel vm = new UserAddressDetailsViewModel
			{
				DeliveryAddress = addresses.DeliveryAddress,
				PostCode = addresses.PostalAddress,
				State = addresses.StateAddress,
				AddressId = addresses.UserDeliveryAddressId
			};

			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateAddressDetails(UserAddressDetailsViewModel vm)
		{

			if (ModelState.IsValid)
			{
				var user = await _userManagerService.GetUserAsync(User);
				UserDeliveryAddress deliveryAddress = new UserDeliveryAddress
				{
					ApplicationUserId = user.Id,
					StateAddress = vm.State,
					DeliveryAddress = vm.DeliveryAddress,
					PostalAddress = vm.PostCode,
					UserDeliveryAddressId = vm.AddressId
				};
				await _addressService.Update(deliveryAddress);
				return RedirectToAction("Details", "User");
			}

			return View(vm);
		}
		[HttpGet]
		public IActionResult AddNewAddress()
		{
	

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddNewAddress(UserAddressDetailsViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManagerService.GetUserAsync(User);
				UserDeliveryAddress deliveryAddress = new UserDeliveryAddress
				{
					ApplicationUserId = user.Id,
					StateAddress = vm.State,
					PostalAddress = vm.PostCode,
					DeliveryAddress = vm.DeliveryAddress
				};
				await _addressService.Create(deliveryAddress);
				return RedirectToAction("Details", "User");
			}
			return View(vm);
		}

		[HttpPost]
        public async Task<IActionResult> Details(UserDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByNameAsync(User.Identity.Name);

                
                user.PhoneNumber = vm.PhoneNumber;
              
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
        public async Task<IActionResult> Cart()
        {
			UserCartViewModel vm;
			var user = await _userManagerService.GetUserAsync(User);
			var addresses = _addressService.Query(a => a.ApplicationUserId == user.Id);

			var addressList = addresses.Select(add => new SelectListItem
			{
				Text = add.DeliveryAddress,
				Value = add.UserDeliveryAddressId.ToString()
			});

			const string keyName = "cartData";
			var data = HttpContext.Session.GetString(keyName);
			List<MapCartData> cartDatas = new List<MapCartData>();
			if (string.IsNullOrEmpty(data))
			{
				vm = new UserCartViewModel
				{
					mapCartDatas = null,
					Addresses = addressList
					
				};
				return View(vm);
			}
			else
			{
				var cache = HttpContext.Session.GetString(keyName);
				cartDatas = JsonConvert.DeserializeObject<List<MapCartData>>(cache);

			}
			 vm = new UserCartViewModel
			{
				mapCartDatas = cartDatas,
				Addresses = addressList
			};

			return View(vm);
        }
		[HttpPost]
		public IActionResult AddToCart(int id, int q)
		{
			if(q <= 0 || id <= 0)
			{
				return RedirectToAction("Index", "Home");
			}
			
			if (ModelState.IsValid)
			{
				var hamper = _hamperService.GetSingle(h => h.HamperId == id);
				if (hamper == null)
				{
					return NotFound();
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
            return View(vm);
        }
		[HttpPost]
	public IActionResult DeleteCartItem(int id)
		{
			const string keyName = "cartData";
			var data = HttpContext.Session.GetString(keyName);
			List<MapCartData> cartDatas;
			if (string.IsNullOrEmpty(data))
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				var cache = HttpContext.Session.GetString(keyName);
				cartDatas = JsonConvert.DeserializeObject<List<MapCartData>>(cache);

			}
			MapCartData cartData = cartDatas.SingleOrDefault(c => c.HamperId == id);
			if(cartData == null)
			{
				return NotFound();
			}
			cartDatas.Remove(cartData);
			HttpContext.Session.SetString(keyName, JsonConvert.SerializeObject(cartDatas));
			return RedirectToAction("Cart", "User");
		}
		[HttpPost]
		public IActionResult UpdateCartItem(int id, int q)
		{
			if (ModelState.IsValid)
			{
			const string keyName = "cartData";
			var data = HttpContext.Session.GetString(keyName);
			List<MapCartData> cartDatas;
			if (string.IsNullOrEmpty(data))
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				var cache = HttpContext.Session.GetString(keyName);
				cartDatas = JsonConvert.DeserializeObject<List<MapCartData>>(cache);

			}
			MapCartData cartData = cartDatas.SingleOrDefault(c => c.HamperId == id);
			if (cartData == null)
			{
				return NotFound();
			}
			cartDatas.Remove(cartData);
			cartData.Quantity = q;
			cartDatas.Add(cartData);

			HttpContext.Session.SetString(keyName, JsonConvert.SerializeObject(cartDatas));

			return RedirectToAction("Cart", "User");
		}
			return RedirectToAction("Cart", "User");
		}
		[HttpPost]
		public async Task<IActionResult> PurchaseCart(string AddressId)
		{
			if (ModelState.IsValid)
			{
				const string keyName = "cartData";
				var data = HttpContext.Session.GetString(keyName);
				List<MapCartData> cartDatas;
				if (string.IsNullOrEmpty(data))
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					var cache = HttpContext.Session.GetString(keyName);
					cartDatas = JsonConvert.DeserializeObject<List<MapCartData>>(cache);

				}
				bool IsId = int.TryParse(AddressId, out int id);
				if (!IsId)
				{
					return NotFound();
				}
				Guid guid = Guid.NewGuid();
				CartInvoice cartInvoice = new CartInvoice
				{
					UserDeliveryAddressId = id,
					purchaseConfirmed = true,
					CartInvoiceId = guid,
				};
				await _invoiceService.Create(cartInvoice);
				IEnumerable<Cart> Carts = cartDatas.Select(cd => new Cart
				{
					HamperId = cd.HamperId,
					Quantity = cd.Quantity,
					CartInvoiceId = guid,


				});
				await _cartService.AddMany(Carts);
				HttpContext.Session.Clear();

			}
			
			return RedirectToAction("Cart", "User");
		}

		[HttpGet]
		public async Task<IActionResult> Feedback()
		{
			var user = await _userManagerService.GetUserAsync(User);
			var addresses = _addressService.Query(add => add.ApplicationUserId == user.Id);
			var invoices = _invoiceService.Query(inv => addresses.Any(addr => addr.UserDeliveryAddressId == inv.UserDeliveryAddressId));
			//var hampers = _hamperService.Query(h => invoices.Any(iids => iids.HamperId == h.HamperId));

			//var SelectList = hampers.Select(x => new SelectListItem {
			//	Value = x.HamperId.ToString(),
			//	Text = x.HamperName
			//});

			UserFeedbackViewModel vm = new UserFeedbackViewModel
			{
				//hampers = SelectList,
				
			};
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Feedback(UserFeedbackViewModel vm)
		{
			if (ModelState.IsValid)
			{
				bool Isid = int.TryParse(vm.HamperId, out int id);

				Feedback feedback = new Feedback
				{
					HamperId = id,
					Rating = vm.rating,
					UserFeedBack = vm.comment
				};

				await _feedBackService.Create(feedback);
		
				return RedirectToAction("Index", "Home");
			}
			return View(vm);

		}
	}
}