﻿using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class ActivityController : Controller
    {
        private IWebHostEnvironment _enviro;
        public ActivityController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
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
        public IActionResult Edit(CActivityPhoto p)
        {
            HotelOrderContext db = new HotelOrderContext();
            Activity cust = db.Activities.FirstOrDefault(t => t.ActivityId == p.ActivityId);
            if (cust != null)
            {
                if (p.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _enviro.WebRootPath + "/image/" + photoName;
                    p.photo.CopyTo(new FileStream(path, FileMode.Create));
                    cust.ActivityImage = photoName;
                }

                cust.ActivityName = p.ActivityName;            
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
