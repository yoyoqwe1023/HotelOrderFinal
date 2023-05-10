using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace HotelOrderFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
        
            return View();
        }

        [HttpPost]
        public IActionResult setSession(string checkInDate, string checkOutDate)
        {

            DateTime checkIn = DateTime.ParseExact(checkInDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime checkOut = DateTime.ParseExact(checkOutDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (HttpContext.Session.GetString("CHECKINDATE") == null)
                HttpContext.Session.SetString("CHECKINDATE", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            else
                HttpContext.Session.SetString("CHECKINDATE", checkIn.ToString("yyyy-MM-dd"));

            if (HttpContext.Session.GetString("CHECKOUTDATE") == null)
                HttpContext.Session.SetString("CHECKOUTDATE", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            else
                HttpContext.Session.SetString("CHECKOUTDATE", checkOut.ToString("yyyy-MM-dd"));

            return new EmptyResult();
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