using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
