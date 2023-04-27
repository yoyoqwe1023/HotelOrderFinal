using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
