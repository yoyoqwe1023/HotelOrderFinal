using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HotelOrderFinal.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            string checkInDateStr = HttpContext.Session.GetString("CHECKINDATE");
            string checkOutDateStr = HttpContext.Session.GetString("CHECKOUTDATE");

            DateTime checkIn;
            DateTime checkOut;

            if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
            {
                checkIn = DateTime.ParseExact(checkInDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
                checkOut = DateTime.ParseExact(checkOutDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            }
            else
            {
                checkIn = DateTime.Now;
                checkOut = DateTime.Now.AddDays(1);
            }

            ViewBag.CheckInDate = checkIn;
            ViewBag.CheckOutDate = checkOut;

            return View();
        }

        public IActionResult Detail()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Member");
            }
            return View();
        }

        public IActionResult Create()
        {
            
            return View();
        }

        public IActionResult ShowOrder()
        {
            return View();
        }
    }
}
