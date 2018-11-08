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

namespace FinalProject_Dips2.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
     
        private SignInManager<IdentityUser> _signInManagerService;
        public UserController(UserManager<IdentityUser> userManager,
                               
                                 SignInManager<IdentityUser> signinManger)
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
                var user = await _userManagerService.FindByNameAsync(vm.UserName);

                if (user != null && await _userManagerService.CheckPasswordAsync(user, vm.Password))
                {
                    await _signInManagerService.PasswordSignInAsync(vm.UserName, vm.Password, true, lockoutOnFailure: false);
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, vm.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Name, vm.UserName));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = vm.RememberMe });
                    return RedirectToAction("Index", "Home");
                  
                }
                ModelState.AddModelError("", "Wrong Email/Password");
                return View();
            }
            
            return View(vm);
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
          public async Task<IActionResult> Register(UserRegisterViewModel vm)
        {
            if(ModelState.IsValid){
               
                var user = await _userManagerService.FindByNameAsync(vm.UserName);

                if (user != null)
                {
                  ModelState.AddModelError("", "User Already Exists");
                    return View(vm);
                }
                 user = new IdentityUser(vm.UserName);

                user.Email = vm.Email;

                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
                    IdentityResult result2 = await _userManagerService.AddToRoleAsync(user, "Customer");
                if(result.Succeeded && result2.Succeeded)
                {
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
        public IActionResult Details()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }
     
    }
}