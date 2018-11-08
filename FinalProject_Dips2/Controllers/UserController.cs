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
namespace FinalProject_Dips2.Controllers
{
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
     // private RoleManager<IdentityRole> _roleManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        public UserController(UserManager<IdentityUser> userManager,
                                //RoleManager<IdentityRole> RoleService,
                                 SignInManager<IdentityUser> signinManger)
        {
            _userManagerService = userManager;
            _signInManagerService = signinManger;
            //_roleManagerService = RoleService;
        }
        public IActionResult Login()
        {
            return View();
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
                IdentityUser user = new IdentityUser(vm.UserName);

                user.Email = vm.Email;
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
                if(result.Succeeded)
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
                    }
                }


            return RedirectToAction("Index","Home");    
            }else{
            return View(vm);
            }
            
        }
        public IActionResult Cart()
        {
            return View();
        }
    }
}