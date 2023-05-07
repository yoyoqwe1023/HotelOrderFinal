using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
