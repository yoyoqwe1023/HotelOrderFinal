using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult List()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.Activities
                        select c;
            return View(datas);
        }
        public IActionResult Edit(int? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            Activity cust = db.Activities.FirstOrDefault(t => t.ActivityId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(Activity p)
        {
            HotelOrderContext db = new HotelOrderContext();
            Activity cust = db.Activities.FirstOrDefault(t => t.ActivityId == p.ActivityId);
            if (cust != null)
            {
               
                cust.ActivityName = p.ActivityName;
                cust.ActivityImage= p.ActivityImage;
                cust.ActivityDirections = p.ActivityDirections;
                cust.ActivityTime = p.ActivityTime;
                cust.ActivityPlace = p.ActivityPlace;
                cust.ActivityPeople = p.ActivityPeople;
                cust.ActivityCost = p.ActivityCost;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}
