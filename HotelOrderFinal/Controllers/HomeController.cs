using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelOrderFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string message = null)
        {/*string message = null*/
            if (!string.IsNullOrEmpty(message))
            {
                TempData["Message"] = message;
            }


            //ViewBag.Message = message;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    
}
}