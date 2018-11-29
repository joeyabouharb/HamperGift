using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Project_UI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ApplicationError()
        {
            return View();
        }

		public IActionResult AuthError()
		{
			 return View();
		}
    }
}