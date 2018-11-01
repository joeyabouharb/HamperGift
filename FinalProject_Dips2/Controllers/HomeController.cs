using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject_Dips2.Models;
using FinalProject_Dips2.ViewModels;
using FinalProject_Dips2.services;


namespace FinalProject_Dips2.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
   
    public HomeController(IDataService<Hamper> HamperDataService)
        {
            _hamperDataService = HamperDataService;
           
        } 
        public IActionResult Index(HomeIndexViewModel vm)
        {
            IEnumerable<Hamper> hampers = _hamperDataService.GetAll();
            vm = new HomeIndexViewModel
            {
             Hampers = hampers
            };
            return View(vm);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}