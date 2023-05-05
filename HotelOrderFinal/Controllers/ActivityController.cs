using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult List()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.Activity
                        select c;
            return View(datas);
        }
    }
}
