using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject_Dips2.services;
using FinalProject_Dips2.Models;
using FinalProject_Dips2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Threading;

namespace FinalProject_Dips2.Controllers
{
    [Authorize(Roles ="Customer")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManagerService;
     
        private SignInManager<ApplicationUser> _signInManagerService;
        public UserController(UserManager<ApplicationUser> userManager,
                               
                                 SignInManager<ApplicationUser> signinManger)
        {
            _userManagerService = userManager;
            _signInManagerService = signinManger;
            
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
                  
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, vm.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Name, vm.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Customer"));
                    var principal = new ClaimsPrincipal(identity);
                   
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = vm.RememberMe });
                    return RedirectToAction("Index", "Home");
                  
                }
                if (login.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your acount is Locked out!");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Wrong Email/Password");
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
           
            return View();
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