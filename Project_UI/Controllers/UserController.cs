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

		private IDataService<Invoice> _invoiceService;

		private IDataService<UserDeliveryAddress> _addressService;
		private IDataService<Feedback> _feedBackService;

        public UserController(UserManager<ApplicationUser> userManager,
                               
                                 SignInManager<ApplicationUser> signinManger,
								IDataService<Invoice> invoiceService,
								IDataService<Hamper> hamperService,
								IDataService<UserDeliveryAddress> adressService,
								IDataService<Feedback> feedbackService
								)
        {
            _userManagerService = userManager;
            _signInManagerService = signinManger;
			_invoiceService = invoiceService;
			_hamperService = hamperService;
			_addressService = adressService;
			_feedBackService = feedbackService;
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

			return RedirectToAction("Details", "User");
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
			const string keyName = "session";
			var data = HttpContext.Session.GetString(keyName);

			if (string.IsNullOrEmpty(data))
			{
				Guid session = Guid.NewGuid();

				HttpContext.Session.SetString("session", session.ToString());
			}
			IEnumerable<Invoice> invoiceNo = _invoiceService.Query(inv => inv.SessionKey == data).Where(ii => ii.Purchased == false);
			
			
			UserCartViewModel vm = new UserCartViewModel();
			if (invoiceNo.Count() == 0)
			{
				return View(vm);
			}
			IQueryable<Hamper> hampers = _hamperService.Query(h => invoiceNo.Any(i => i.HamperId == h.HamperId));
			List<MapCartData> cartDatas = new List<MapCartData>();
			MapCartData map = new MapCartData();
			
			

			foreach(var hamper in hampers)
			{
				foreach(var invoice in invoiceNo)
					{
						
					
					if(hamper.HamperId == invoice.HamperId)
					{
						map = new MapCartData
						{
							InvoiceId = invoice.InvoiceId,
							HamperName = hamper.HamperName,
							Cost = (hamper.Cost * invoice.Quantity),
							Quantity = invoice.Quantity
						};

					}
					if (cartDatas.Contains(map))
					{
						continue;
					}
						cartDatas.Add(map);
					

					}
				}
			var user = await _userManagerService.GetUserAsync(User);
			var addresses = _addressService.Query(a => a.ApplicationUserId == user.Id).ToList();
		
			var addressSelect = addresses.Select(adr => new SelectListItem
			{
				Value = adr.UserDeliveryAddressId.ToString(),
				Text = adr.DeliveryAddress

			});
			vm = new UserCartViewModel
			{
			mapCartDatas = cartDatas,
			Addresses = addressSelect

			};

			return View(vm);
        }
		[HttpPost]
		public async Task<IActionResult> AddToCart(int id)
		{
			const string keyName = "session";
			var data = HttpContext.Session.GetString(keyName);

			if (string.IsNullOrEmpty(data))
			{
				Guid session = Guid.NewGuid();

				HttpContext.Session.SetString("session", session.ToString());
			}

			bool tryParse = int.TryParse(Request.Form["quantity"], out int q);
			if (tryParse == false)
			{
				//ModelState.AddModelError("", "Please enter a valid number");
				return RedirectToAction("Cart", "User");
			}
			if (_invoiceService.Query(x => x.SessionKey== data).
				Where(inv => inv.HamperId == id).
				Where(h => h.Purchased == false).Count() != 0)
			{
				//ModelState.AddModelError("", "This item already has been added to cart.");
				return RedirectToAction("Cart", "User");
			}

			var user = await _userManagerService.GetUserAsync(User);		
			Invoice invoice = new Invoice
			{

				HamperId = id,
				SessionKey = data,
				UserDeliveryAddressId = _addressService.Query(a => a.ApplicationUserId == user.Id).First().UserDeliveryAddressId,
				Quantity = q,
				Purchased = false
			};
			await _invoiceService.Create(invoice);
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
            return View(vm);
        }
		[HttpPost]
	public IActionResult DeleteCartItem(int id)
		{
			var item = _invoiceService.GetSingle(inv => inv.InvoiceId == id);
			_invoiceService.Delete(item);
			return RedirectToAction("Cart", "User");
		}
		[HttpPost]
		public IActionResult UpdateCartItem(int id)
		{
			var item = _invoiceService.GetSingle(inv => inv.InvoiceId == id);
			int q;
			bool tryParse = int.TryParse(Request.Form["quantity"], out q);
			if(tryParse == true)
			{
				item.Quantity = q;
				_invoiceService.Update(item);

				return RedirectToAction("Cart", "User");
			}
			
			
			return RedirectToAction("Cart","User");
		}
		[HttpPost]
		public async Task<IActionResult> PurchaseCart(string AddressId)
		{
			bool isId = int.TryParse(AddressId, out int x);

			if (!isId)
			{
				return NotFound();
			}

			const string keyName = "session";
			var data = HttpContext.Session.GetString(keyName);
			
			if (string.IsNullOrEmpty(data))
			{
				Guid session = new Guid();

				HttpContext.Session.SetString("session", session.ToString());
			}
			string applicationUser = _userManagerService.GetUserId(User);
			
			List<Invoice> invoiceNo = _invoiceService.Query(inv => inv.SessionKey == data).ToList();
			invoiceNo.ForEach(item =>
			{ item.Purchased = true; item.UserDeliveryAddressId = x; });

			await _invoiceService.UpdateMany(invoiceNo);
	

			return RedirectToAction("Cart", "User");
		}

		[HttpGet]
		public async Task<IActionResult> Feedback()
		{
			var user = await _userManagerService.GetUserAsync(User);
			var addresses = _addressService.Query(add => add.ApplicationUserId == user.Id);
			var invoices = _invoiceService.Query(inv => addresses.Any(addr => addr.UserDeliveryAddressId == inv.UserDeliveryAddressId));
			var hampers = _hamperService.Query(h => invoices.Any(iids => iids.HamperId == h.HamperId));

			var SelectList = hampers.Select(x => new SelectListItem {
				Value = x.HamperId.ToString(),
				Text = x.HamperName
			});

			UserFeedbackViewModel vm = new UserFeedbackViewModel
			{
				hampers = SelectList,
				
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