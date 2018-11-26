using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Infastructure.Models;
using Project_Infastructure.services;
using Project_UI.ViewModels;

namespace Project_UI.Controllers
{
    public class AccountController : Controller
    {
		private UserManager<ApplicationUser> _userManagerService;

		private SignInManager<ApplicationUser> _signInManagerService;

		private IDataService<UserDeliveryAddress> _addressService;
		public AccountController(UserManager<ApplicationUser> userManager,
		 SignInManager<ApplicationUser> signInManager,
		IDataService<UserDeliveryAddress> addressService)
		{
			_signInManagerService = signInManager;
			_userManagerService = userManager;
			_addressService = addressService;
		}


		[HttpGet]
		public IActionResult Login()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel vm)
		{
			if (ModelState.IsValid)
			{

				var login = await _signInManagerService.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, lockoutOnFailure: true);
				if (login.Succeeded)
				{
					ClaimsIdentity identity;
					var user = await _userManagerService.FindByNameAsync(vm.UserName);
					var role = await _userManagerService.GetRolesAsync(user);
					if (role.Contains("Customer"))
					{
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
					else if (role.Contains("Admin"))
					{
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
			if (ModelState.IsValid)
			{

				var user = await _userManagerService.FindByNameAsync(vm.UserName);

				if (user != null)
				{
					ModelState.AddModelError("", "User Already Exists");
					return View(vm);
				}
				user = new ApplicationUser
				{

					UserName = vm.UserName,

				};

				user.Email = vm.Email;
				user.PhoneNumber = vm.PhoneNumber;


				IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);

				if (result.Succeeded)
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
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManagerService.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}