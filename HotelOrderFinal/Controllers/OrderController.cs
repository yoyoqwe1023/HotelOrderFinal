using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
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
